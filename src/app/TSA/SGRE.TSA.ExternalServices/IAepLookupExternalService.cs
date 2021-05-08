using SGRE.TSA.Models;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IAepLookupExternalService
    {
        Task<ExternalServiceResponse<SSTAepLookupGross>> PutAepAsync(SSTAepLookupGross aepLookupGross);
    }
}
