using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NSI.BusinessLogic.Interfaces;
using NSI.Common.Serialization;
using NSI.Common.Utilities;
using NSI.DataContracts.Oasis.Models;
using NSI.DataContracts.Oasis.Requests;
using NSI.DataContracts.Oasis.Responses;
using NSI.Proxy;

namespace NSI.BusinessLogic.Implementations
{
    public class BlockchainManipulation : IBlockchainManipulation
    {
        private readonly string _algorithm;
        private readonly string _authBaseUrl;
        private readonly string _baseUrl;
        private readonly string _clientId;
        private readonly string _scope;
        private readonly string _storageBaseUrl;
        private readonly IConfigurationSection _privateKey;

        public BlockchainManipulation(IConfiguration configuration)
        {
            _algorithm = configuration["Oasis:Algorithm"];
            _authBaseUrl = configuration["Oasis:AuthBaseUrl"];
            _baseUrl = configuration["Oasis:BaseUrl"];
            _clientId = configuration["Oasis:ClientId"];
            _scope = configuration["Oasis:Scope"];
            _storageBaseUrl = configuration["Oasis:StorageBaseUrl"];
            _privateKey = configuration.GetSection("Oasis:PrivateKey");
        }

        private string MakeToken()
        {
            JsonWebKey jwk = new JsonWebKey
            {
                Kty = _privateKey["kty"],
                Kid = _privateKey["kid"],
                Use = _privateKey["use"],
                Alg = _privateKey["alg"],
                Crv = _privateKey["crv"],
                X = _privateKey["x"],
                Y = _privateKey["y"],
                D = _privateKey["d"]
            };

            var timestamp = Convert.ToInt32((DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds);
            var token = new JwtSecurityToken
            (
                null,
                null,
                new[] {
                    new Claim(JwtRegisteredClaimNames.Aud, _authBaseUrl),
                    new Claim(JwtRegisteredClaimNames.Exp, Convert.ToString(timestamp + 3600), ClaimValueTypes.Integer),
                    new Claim("iat", Convert.ToString(timestamp - 30), ClaimValueTypes.Integer),
                    new Claim(JwtRegisteredClaimNames.Iss, _clientId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()[..12]),
                    new Claim(JwtRegisteredClaimNames.Sub, _clientId)
                },
                null,
                null,
                new SigningCredentials(jwk, _algorithm)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<T> Login<T>()
        {
            var token = MakeToken();
            var body = new LoginRequest
            {
                Audience = _baseUrl,
                GrantType = "client_credentials",
                ClientId = _clientId,
                ClientAssertionType = "urn:ietf:params:oauth:client-assertion-type:jwt-bearer",
                ClientAssertion = token,
                Scope = _scope
            };
            var json = JsonHelper.Serialize(body, false);
            var form = JsonHelper.Deserialize<Dictionary<string, string>>(json);

            var content = await RestClient.Instance().Send
            (
                _authBaseUrl,
                HttpMethod.Post,
                new FormUrlEncodedContent(form),
                "application/json"
            );

            return JsonHelper.Deserialize<T>(content, false);
        }

        private async Task<T> UploadDocument<T>(string token, string name, Stream stream, string[] tags = null)
        {
            tags ??= Array.Empty<string>();
            var request = new DocumentUploadRequest
            {
                Details = new Document
                {
                    Title = name,
                    Tags = tags
                }
            };

            using var content = new MultipartFormDataContent();
            
            var hashStream = new MemoryStream(Encoding.UTF8.GetBytes(HashHelper.ComputeFileHash(stream)));
            using var data = new StreamContent(hashStream);
            data.Headers.Add("Content-Disposition", "form-data; name=\"data\"; filename=\"" + name + ".pdf\"");
            content.Add(data, "data");

            var details = JsonHelper.Serialize(request, false);
            var metadata = new StringContent(details);
            metadata.Headers.Add("Content-Disposition", "form-data; name=\"metadata\"");
            content.Add(metadata, "metadata");
                
            var response = await RestClient.Instance().Send
            (
                _storageBaseUrl,
                HttpMethod.Post,
                content,
                "application/json",
                token
            );
            
            stream.Close();
            hashStream.Close();
            return JsonHelper.Deserialize<T>(response);
        }

        private async Task<string> DownloadDocument(string token, string documentId)
        {
            var response = await RestClient.Instance().Download
            (
                $"{_baseUrl}/v1/documents/{documentId}/download",
                HttpMethod.Get,
                null,
                null,
                token
            );

            return response.Trim();
        }

        public async Task<string> UploadDocument(string documentId, Stream stream)
        {
            var token = (await Login<LoginResponse>()).AccessToken;
            var response = await UploadDocument<DocumentResponse>(token, documentId, stream);
            return response.Id;
        }

        public async Task<bool> ValidateDocument(string blockchainId, string documentHash)
        {
            var token = (await Login<LoginResponse>()).AccessToken;
            var blockchainHash = await DownloadDocument(token, blockchainId);
            return blockchainHash.Equals(documentHash);
        }
    }
}
