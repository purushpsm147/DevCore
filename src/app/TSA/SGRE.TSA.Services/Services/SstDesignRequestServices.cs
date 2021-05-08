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
    public class SstDesignRequestServices : ISstDesignRequestServices
    {
        private readonly IExternalServiceFactory externalServiceFactory;
        private readonly ILogger<SstDesignRequestServices> logger;

        public SstDesignRequestServices(IExternalServiceFactory externalServiceFactory, ILogger<SstDesignRequestServices> logger)
        {
            this.externalServiceFactory = externalServiceFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<SstDesignRequest> SstDesignRequest)> GetSstDesignRequestByIdAsync(int SstId)
        {
            var externalService = externalServiceFactory.CreateExternalService<SstDesignRequest>(logger);

            var requestSstDesignResult = await externalService.GetAsync($"?$filter=SstTowerId eq {SstId}");

            if (requestSstDesignResult.IsSuccess)
            {
                var result = new
                {
                    requestDesign = requestSstDesignResult.ResponseData
                };
                return (true, result.requestDesign);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic SstDesignRequest)> PutSstDesignRequestAsync(SstDesignRequest sstDesignRequest)
        {
            var externalService = externalServiceFactory.CreateExternalService<SstDesignRequest>(logger);

            var designRequestResult = await externalService.PutAsync(sstDesignRequest);

            if (designRequestResult.IsSuccess)
            {
                var result = new
                {
                    designRequestResult = designRequestResult.ResponseData
                };
                return (true, result);
            }

            return (false, designRequestResult.ErrorMessage);
        }
    }
}
