using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class WtgCatalogueService : IWtgCatalogueService
    {
        private readonly ExternalServices.IWtgCatalogueExternalService catalogueService;
        public WtgCatalogueService(ExternalServices.IWtgCatalogueExternalService catalogueService)
        {
            this.catalogueService = catalogueService;
        }

        public async Task<(bool IsSuccess, IEnumerable<WtgCatalogue> wtgCatalogueResult)> GetWtgCatalogueAsync()
        {
            var catalogueResult = await catalogueService.GetWtgCatalogueAsync();
            if (catalogueResult.IsSuccess)
            {
                return (true, catalogueResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, WtgCatalogue wtgCatalogueResult)> GetWtgCatalogueAsync(int id)
        {
            var catalogueResult = await catalogueService.GetWtgCatalogueAsync(id);
            if (catalogueResult.IsSuccess)
            {
                return (true, catalogueResult.ResponseData);
            }

            return (false, null);
        }
        public async Task<(bool IsSuccess, WtgThreshold wtgThresholdResult)> GetWtgThresholdAsync()
        {
            var wtgThresholdResult = await catalogueService.GetWtgThresholdAsync();
            if (wtgThresholdResult.IsSuccess)
            {
                return (true, wtgThresholdResult.ResponseData);
            }

            return (false, null);
        }

    }
}
