using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostsTowerConstructionLeadTimeService : ICostsTowerConstructionLeadTimeService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<CostsTowerConstructionLeadTimeService> _logger;

        public CostsTowerConstructionLeadTimeService(IExternalServiceFactory externalServiceFactory, ILogger<CostsTowerConstructionLeadTimeService> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerConstructionLeadTimeMeta> costsTowerConstructionLeadTimeMetaResult)> GetCostsTowerConstructionLeadTimeMetaAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerConstructionLeadTimeMeta>(_logger);

            var costsTowerExWorksResult = await externalService.GetAsync("");

            if (costsTowerExWorksResult.IsSuccess)
            {
                return (true, costsTowerExWorksResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerConstructionLeadTimeMeta> costsTowerConstructionLeadTimeMetaResult)> GetCostsTowerConstructionLeadTimeMetaAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerConstructionLeadTimeMeta>(_logger);

            var costsTowerExWorksResult = await externalService.GetByIdAsync(id);

            if (costsTowerExWorksResult.IsSuccess)
            {
                return (true, costsTowerExWorksResult.ResponseData);
            }

            return (false, null);
        }
    }
}
