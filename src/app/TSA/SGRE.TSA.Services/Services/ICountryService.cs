using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICountryService
    {
        Task<(bool IsSuccess, IEnumerable<Country> countryResult)> GetCountryAsync();
        Task<(bool IsSuccess, dynamic countryResult)> GetCountryAsync(int id);
        Task<(bool IsSuccess, dynamic countryResult)> GetCountryByRegionAsync(int regionId);
    }
}
