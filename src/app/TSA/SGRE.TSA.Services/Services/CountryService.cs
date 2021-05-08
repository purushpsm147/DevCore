using SGRE.TSA.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly ExternalServices.ICountryExternalService countryService;
        public CountryService(ExternalServices.ICountryExternalService countryService)
        {
            this.countryService = countryService;
        }
        public async Task<(bool IsSuccess, IEnumerable<Country> countryResult)> GetCountryAsync()
        {
            var countryResult = await countryService.GetCountryAsync();
            if (countryResult.IsSuccess)
            {
                return (true, countryResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic countryResult)> GetCountryAsync(int id)
        {
            var countryResult = await countryService.GetCountryAsync(id);
            if (countryResult.IsSuccess)
            {
                var result = new
                {
                    country = countryResult.ResponseData
                };
                return (true, result);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic countryResult)> GetCountryByRegionAsync(int regionId)
        {
            var countryResult = await countryService.GetCountryByRegionAsync(regionId);
            if (countryResult.IsSuccess)
            {
                var result = new
                {
                    country = countryResult.ResponseData
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}
