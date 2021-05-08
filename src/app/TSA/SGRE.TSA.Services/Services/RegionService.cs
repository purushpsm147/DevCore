using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class RegionService : IRegionService
    {
        private readonly ExternalServices.IRegionExternalService regionService;
        public RegionService(ExternalServices.IRegionExternalService regionService)
        {
            this.regionService = regionService;
        }

        public async Task<(bool IsSuccess, IEnumerable<Region> regionResult)> GetRegionAsync()
        {
            var regionResult = await regionService.GetRegionsAsync();
            if (regionResult.IsSuccess)
            {

                return (true, regionResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic regionResult)> GetRegionAsync(int id)
        {
            var regionResult = await regionService.GetRegionsAsync(id);
            if (regionResult.IsSuccess)
            {
                var result = new
                {
                    region = regionResult.ResponseData
                };
                return (true, result);
            }

            return (false, null);
        }

    }
}
