using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostsTowerLogisticsLeadTimeService : ICostsTowerLogisticsLeadTimeService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<CostsTowerLogisticsLeadTimeService> _logger;

        public CostsTowerLogisticsLeadTimeService(IExternalServiceFactory externalServiceFactory, ILogger<CostsTowerLogisticsLeadTimeService> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerLogisticsLeadTimeMeta> costsTowerLogisticsLeadTimeMetaResult)> GetCostsTowerLogisticsLeadTimeMetaAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerLogisticsLeadTimeMeta>(_logger);

            var costsTowerLogisticsLeadTimeMetaResult = await externalService.GetAsync("");

            if (costsTowerLogisticsLeadTimeMetaResult.IsSuccess)
            {
                return (true, costsTowerLogisticsLeadTimeMetaResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerLogisticsLeadTimeMeta> costsTowerLogisticsLeadTimeMetaResult)> GetCostsTowerLogisticsLeadTimeMetaAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerLogisticsLeadTimeMeta>(_logger);

            var costsTowerLogisticsLeadTimeMetaResult = await externalService.GetByIdAsync(id);

            if (costsTowerLogisticsLeadTimeMetaResult.IsSuccess)
            {
                return (true, costsTowerLogisticsLeadTimeMetaResult.ResponseData);
            }

            return (false, null);
        }
    }
}
