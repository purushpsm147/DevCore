using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IRegionExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Region>>> GetRegionsAsync();
        Task<ExternalServiceResponse<IEnumerable<Region>>> GetRegionsAsync(int id);
    }
}
