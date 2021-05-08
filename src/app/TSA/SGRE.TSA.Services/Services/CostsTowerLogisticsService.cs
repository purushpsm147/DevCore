using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostsTowerLogisticsService : ICostsTowerLogisticsService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<CostsTowerLogisticsService> _logger;

        public CostsTowerLogisticsService(IExternalServiceFactory externalServiceFactory, ILogger<CostsTowerLogisticsService> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerLogisticsMeta> costsTowerLogisticsMetaResult)> GetCostsTowerLogisticsMetaAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerLogisticsMeta>(_logger);

            var costsTowerLogisticsMetaResult = await externalService.GetAsync("");

            if (costsTowerLogisticsMetaResult.IsSuccess)
            {
                return (true, costsTowerLogisticsMetaResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerLogisticsMeta> costsTowerLogisticsMetaResult)> GetCostsTowerLogisticsMetaAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerLogisticsMeta>(_logger);

            var costsTowerLogisticsMetaResult = await externalService.GetByIdAsync(id);

            if (costsTowerLogisticsMetaResult.IsSuccess)
            {
                return (true, costsTowerLogisticsMetaResult.ResponseData);
            }

            return (false, null);
        }
    }
}
