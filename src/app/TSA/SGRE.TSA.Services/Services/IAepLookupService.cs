using SGRE.TSA.Models;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IAepLookupService
    {
        Task<(bool IsSuccess, dynamic AepResults)> PutAepAsync(SSTAepLookupGross aepLookupGross);
        Task<(bool IsSuccess, dynamic AepResults)> GetAepAsync(string aepGuid);
    }
}
