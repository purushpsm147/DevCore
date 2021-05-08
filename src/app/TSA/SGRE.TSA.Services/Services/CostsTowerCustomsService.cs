using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostsTowerCustomsService : ICostsTowerCustomsService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<CostsTowerCustomsService> _logger;

        public CostsTowerCustomsService(IExternalServiceFactory externalServiceFactory, ILogger<CostsTowerCustomsService> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerCustomsMeta> costsTowerCustomsMetaResult)> GetCostsTowerCustomsMetaAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerCustomsMeta>(_logger);

            var costsTowerCustomsMetaResult = await externalService.GetAsync("");

            if (costsTowerCustomsMetaResult.IsSuccess)
            {
                return (true, costsTowerCustomsMetaResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerCustomsMeta> costsTowerCustomsMetaResult)> GetCostsTowerCustomsMetaAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerCustomsMeta>(_logger);

            var costsTowerCustomsMetaResult = await externalService.GetByIdAsync(id);

            if (costsTowerCustomsMetaResult.IsSuccess)
            {
                return (true, costsTowerCustomsMetaResult.ResponseData);
            }

            return (false, null);
        }
    }
}
