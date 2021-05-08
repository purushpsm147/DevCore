using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostsTowerConstructionService : ICostsTowerConstructionService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<CostsTowerConstructionService> _logger;

        public CostsTowerConstructionService(IExternalServiceFactory externalServiceFactory, ILogger<CostsTowerConstructionService> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerConstructionMeta> costsTowerConstructionMetaResult)> GetCostsTowerConstructionMetaAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerConstructionMeta>(_logger);

            var costsTowerConstructionMetaResult = await externalService.GetAsync("");

            if (costsTowerConstructionMetaResult.IsSuccess)
            {
                return (true, costsTowerConstructionMetaResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerConstructionMeta> costsTowerConstructionMetaResult)> GetCostsTowerConstructionMetaAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerConstructionMeta>(_logger);

            var costsTowerConstructionMetaResult = await externalService.GetByIdAsync(id);

            if (costsTowerConstructionMetaResult.IsSuccess)
            {
                return (true, costsTowerConstructionMetaResult.ResponseData);
            }

            return (false, null);
        }
    }
}
