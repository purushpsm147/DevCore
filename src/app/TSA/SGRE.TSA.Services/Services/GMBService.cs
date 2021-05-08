using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class GMBService : IGMBService
    {
        private readonly IGMBExternalService gMBExternalService;

        public GMBService(IGMBExternalService gMBExternalService)
        {
            this.gMBExternalService = gMBExternalService;
        }
        public async Task<(bool IsSuccess, IEnumerable<GenericMarketBoundary> genericMarketBoundaries)> GetGMBAsync(int CountryId)
        {
            var gmbServiceResponse = await gMBExternalService.GetGMBAsync(CountryId);
            if (gmbServiceResponse.IsSuccess)
            {
                return (true, gmbServiceResponse.ResponseData);
            }

            return (false, null);
        }
    }
}
