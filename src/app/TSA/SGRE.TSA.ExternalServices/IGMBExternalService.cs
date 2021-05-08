using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IGMBExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<GenericMarketBoundary>>> GetGMBAsync(int CountryId);
    }
}
