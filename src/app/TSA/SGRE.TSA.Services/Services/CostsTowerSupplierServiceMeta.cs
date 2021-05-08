using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostsTowerSupplierServiceMeta : ICostsTowerSupplierServiceMeta
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<CostsTowerSupplierMeta> _logger;

        public CostsTowerSupplierServiceMeta(IExternalServiceFactory externalServiceFactory, ILogger<CostsTowerSupplierMeta> logger)
        {
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerSupplierMeta> costsTowerSupplierResult)> GetCostsTowerSupplierAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerSupplierMeta>(_logger);

            var costsTowerSupplierResult = await externalService.GetAsync("");

            if (costsTowerSupplierResult.IsSuccess)
            {
                return (true, costsTowerSupplierResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostsTowerSupplierMeta> costsTowerSupplierResult)> GetCostsTowerSupplierAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostsTowerSupplierMeta>(_logger);

            var costsTowerSupplierResult = await externalService.GetByIdAsync(id);

            if (costsTowerSupplierResult.IsSuccess)
            {
                return (true, costsTowerSupplierResult.ResponseData);
            }

            return (false, null);
        }
    }
}
