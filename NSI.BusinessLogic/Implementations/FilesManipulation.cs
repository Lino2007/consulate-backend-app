using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NSI.BusinessLogic.Interfaces;

namespace NSI.BusinessLogic.Implementations
{
    public class FilesManipulation : IFilesManipulation
    {
        private readonly string _endpointUrl;
        private readonly string _bucketName;
        private readonly string _folderName;
        private static AmazonS3Client _client;

        public FilesManipulation(IConfiguration configuration)
        {
            _endpointUrl = configuration["DigitalOcean:EndpointUrl"];
            _bucketName = configuration["DigitalOcean:BucketName"];
            _folderName = configuration["DigitalOcean:FolderName"];
            var credentials = new BasicAWSCredentials(
                configuration["DigitalOcean:AccessKey"],
                configuration["DigitalOcean:SecretKey"]
            );
            var config = new AmazonS3Config
            {
                ServiceURL = _endpointUrl,
                ForcePathStyle = true
            };
            _client = new AmazonS3Client(credentials, config);
        }

        public async Task<string> UploadFile(IFormFile file, string fileName)
        {
            string fileNameWithExtension = fileName + Path.GetExtension(file.FileName);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = _bucketName + "/" + _folderName,
                Key = fileNameWithExtension,
                CannedACL = S3CannedACL.PublicRead
            };

            //uploadRequest.UploadProgressEvent += (s, args) => System.Diagnostics.Debug.WriteLine(args.PercentDone);

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            await using (memoryStream)
            {
                uploadRequest.InputStream = memoryStream;
                var fileTransferUtility = new TransferUtility(_client);
                await fileTransferUtility.UploadAsync(uploadRequest);
            }

            return _endpointUrl + "/" + _bucketName + "/" + _folderName + "/" + fileNameWithExtension;
        }

        public async Task<string> DownloadFile(string fileName)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = _folderName + "/" + fileName
            };
            using GetObjectResponse response = await _client.GetObjectAsync(request);
            await using Stream responseStream = response.ResponseStream;
            using StreamReader reader = new StreamReader(responseStream);
            return await reader.ReadToEndAsync();
        }

        public async Task ListFiles()
        {
            ListObjectsRequest listRequest = new ListObjectsRequest
            {
                BucketName = _bucketName,
                Prefix = _folderName + "/"
            };

            ListObjectsResponse listResponse;
            do
            {
                listResponse = await _client.ListObjectsAsync(listRequest);
                foreach (S3Object obj in listResponse.S3Objects)
                {
                    Console.WriteLine(obj.Key);
                    Console.WriteLine("Size - " + obj.Size);
                }

                listRequest.Marker = listResponse.NextMarker;
            } while (listResponse.IsTruncated);
        }
    }
}
