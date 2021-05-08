using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class WtgCatalogueModelService : IWtgCatalogueModelService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<WtgCatalogueModel> _logger;

        public WtgCatalogueModelService(IExternalServiceFactory externalServiceFactory, ILogger<WtgCatalogueModel> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<WtgCatalogueModel> wtgCatalogueModelResult)> GetWtgCatalogueModelBywtgCatalougeIdAsync(int wtgCatalougeId)
        {
            var externalService = _externalServiceFactory.CreateExternalService<WtgCatalogueModel>(_logger);

            var wtgCatalogueModelResult = await externalService.GetAsync($"?$filter=WtgCatalogueId eq {wtgCatalougeId}");

            if (wtgCatalogueModelResult.IsSuccess)
            {
                return (true, wtgCatalogueModelResult.ResponseData);
            }

            return (false, null);
        }
    }
}
