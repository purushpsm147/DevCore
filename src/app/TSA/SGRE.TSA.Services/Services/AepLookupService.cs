using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class AepLookupService : IAepLookupService
    {
        private readonly IAepLookupExternalService aepLookupExternalService;
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<SSTAepLookupGross> _logger;
        public AepLookupService(IAepLookupExternalService aepLookupExternalService, IExternalServiceFactory externalServiceFactory, ILogger<SSTAepLookupGross> logger)
        {
            this.aepLookupExternalService = aepLookupExternalService;
            _externalServiceFactory = externalServiceFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, dynamic AepResults)> GetAepAsync(string aepGuid)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SSTAepLookupGross>(_logger);

            var aepLookupResult = await externalService.GetAsync($"?$filter=AepLookupUuid eq {aepGuid}&$orderby=Id desc");

            if (aepLookupResult.IsSuccess)
            {
                return (true, aepLookupResult.ResponseData.FirstOrDefault());
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic AepResults)> PutAepAsync(SSTAepLookupGross aepLookupGross)
        {
            aepLookupGross.AepLookupUuid = Guid.NewGuid();
            var AepLookup = await aepLookupExternalService.PutAepAsync(aepLookupGross);
            if (AepLookup.IsSuccess)
            {
                return (true, AepLookup.ResponseData);
            }

            return (false, AepLookup.ErrorMessage);
        }
    }
}