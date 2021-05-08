using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface ICountryExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Country>>> GetCountryAsync();  
        Task<ExternalServiceResponse<IEnumerable<Country>>> GetCountryAsync(int id);  
        Task<ExternalServiceResponse<IEnumerable<Country>>> GetCountryByRegionAsync(int regionId);  
    }
}
