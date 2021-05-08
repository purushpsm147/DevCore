using Microsoft.AspNetCore.Http;
using SGRE.TSA.Models;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IFileUploadService
    {
        Task<(bool IsSuccess, string SendUploadResults)> Upload(IFormFile file, BlobUpload jsonObject);
    }
}
