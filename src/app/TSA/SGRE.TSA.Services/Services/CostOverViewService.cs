using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostOverViewService : ICostOverViewService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<CostOverViewService> _logger;

        public CostOverViewService(IExternalServiceFactory externalServiceFactory, ILogger<CostOverViewService> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostOverViewMeta> costOverViewMetaResult)> GetCostOverViewMetaAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostOverViewMeta>(_logger);

            var CostOverViewMetaResult = await externalService.GetAsync("");

            if (CostOverViewMetaResult.IsSuccess)
            {
                return (true, CostOverViewMetaResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostOverViewMeta> costOverViewMetaResult)> GetCostOverViewMetaAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostOverViewMeta>(_logger);

            var CostOverViewMetaResult = await externalService.GetByIdAsync(id);

            if (CostOverViewMetaResult.IsSuccess)
            {
                return (true, CostOverViewMetaResult.ResponseData);
            }

            return (false, null);
        }

    }
}
