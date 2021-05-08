using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostsTowerExWorksMetaService : ICostsTowerExWorksMetaService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<CostsTowerExWorksMeta> _logger;

        public CostsTowerExWorksMetaService(IExternalServiceFactory externalServiceFactory, ILogger<CostsTowerExWorksMeta> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerExWorksMeta> costsTowerExWorksResult)> GetCostsTowerExWorksAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerExWorksMeta>(_logger);

            var costsTowerExWorksResult = await externalService.GetAsync("");

            if (costsTowerExWorksResult.IsSuccess)
            {
                return (true, costsTowerExWorksResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerExWorksMeta> costsTowerExWorksResult)> GetCostsTowerExWorksAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerExWorksMeta>(_logger);

            var costsTowerExWorksResult = await externalService.GetByIdAsync(id);

            if (costsTowerExWorksResult.IsSuccess)
            {
                return (true, costsTowerExWorksResult.ResponseData);
            }

            return (false, null);
        }
    }
}
