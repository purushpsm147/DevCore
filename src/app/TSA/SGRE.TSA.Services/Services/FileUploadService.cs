using Microsoft.AspNetCore.Http;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IFileUploadExternalService fileUploadExternalService;

        public FileUploadService(IFileUploadExternalService fileUploadExternalService)
        {
            this.fileUploadExternalService = fileUploadExternalService;
        }
        public async Task<(bool IsSuccess, string SendUploadResults)> Upload(IFormFile file, BlobUpload jsonObject)
        {
            var result = await fileUploadExternalService.UploadFile(file, jsonObject);
            if (result.IsSuccess)
            {
                return (true, result.ResponseData);
            }
            return (false, result.ErrorMessage);
        }
    }
}
