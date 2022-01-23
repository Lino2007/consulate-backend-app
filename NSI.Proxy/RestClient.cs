using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Proxy
{
    [ExcludeFromCodeCoverage]
    public class RestClient
    {
        private static RestClient _instance;
        private readonly HttpClient _client;

        private RestClient(HttpClient client)
        {
            _client = client;
        }

        public static RestClient Instance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            
            _instance = new RestClient(new HttpClient());
            return _instance;
        }

        private static HttpRequestMessage CreateRequest(string url, HttpMethod method, HttpContent content, string accept, string token = null)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = method
            };
            
            if (accept != null)
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));
            }

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (content != null)
            {
                request.Content = content;
            }

            return request;
        }

        public async Task<string> Send(string url, HttpMethod method, HttpContent content, string accept, string token = null)
        {
            var request = CreateRequest(url, method, content, accept, token);
            var response = await _client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Download(string url, HttpMethod method, HttpContent content, string accept, string token = null)
        {
            var request = CreateRequest(url, method, content, accept, token);
            var response = await _client.SendAsync(request);
            var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
    }
}
