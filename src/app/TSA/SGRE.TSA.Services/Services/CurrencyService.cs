using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IExternalServiceFactory externalServiceFactory;
        private readonly ILogger<Currency> logger;

        public CurrencyService(IExternalServiceFactory externalServiceFactory, ILogger<Currency> logger)
        {
            this.externalServiceFactory = externalServiceFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Currency> currencyResult)> GetCurrencyAsync()
        {
            var externalService = externalServiceFactory.CreateExternalService<Currency>(logger);

            var currencyResult = await externalService.GetAsync("");

            if (currencyResult.IsSuccess)
            {
                return (true, currencyResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<Currency> currencyResult)> GetCurrencyAsync(int id)
        {
            var externalService = externalServiceFactory.CreateExternalService<Currency>(logger);

            var currencyResult = await externalService.GetByIdAsync(id);

            if (currencyResult.IsSuccess)
            {
                return (true, currencyResult.ResponseData);
            }

            return (false, null);
        }
    }
}
