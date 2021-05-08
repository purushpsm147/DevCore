using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SGRE.TSA.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class FileUploadExternalService : IFileUploadExternalService
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly ILogger<FileUploadExternalService> logger;

        public FileUploadExternalService(BlobServiceClient blobServiceClient, ILogger<FileUploadExternalService> logger)
        {
            this.blobServiceClient = blobServiceClient;
            this.logger = logger;
        }
        public async Task<ExternalServiceResponse<string>> UploadFile(IFormFile file, BlobUpload jsonObject)
        {
            try
            {

                string containerName = "tsa-blob";
                var result = await blobServiceClient.GetBlobContainerClient(containerName).ExistsAsync();
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                if (!result)
                {
                    containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
                }
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                var intFilename = Path.GetFileNameWithoutExtension(jsonObject.FieldName);
                var intFileDescriptor = Path.GetFileNameWithoutExtension(jsonObject.FileDescriptor);
                var _fileName = $"{jsonObject.FileClass}/{jsonObject.ID}_{intFilename}_{intFileDescriptor}{ext}";

                var blobClientResult = await containerClient.GetBlobClient(_fileName).ExistsAsync();
                if (blobClientResult)
                {
                    await containerClient.GetBlobClient(_fileName).DeleteAsync();
                }

                BlobClient blobClient = containerClient.GetBlobClient(_fileName);
                using (var uploadFileStream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(uploadFileStream);
                }

                return new ExternalServiceResponse<string>()
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                    ResponseData = blobClient.Uri.ToString()
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<string>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };

            }
        }
    }
}
