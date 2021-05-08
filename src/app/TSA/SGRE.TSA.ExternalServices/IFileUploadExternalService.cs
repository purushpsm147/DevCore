using Microsoft.AspNetCore.Http;
using SGRE.TSA.Models;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IFileUploadExternalService
    {
        Task<ExternalServiceResponse<string>> UploadFile(IFormFile file, BlobUpload jsonObject);
    }
}
