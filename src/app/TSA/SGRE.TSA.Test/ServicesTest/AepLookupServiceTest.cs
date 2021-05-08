using Microsoft.Extensions.Logging;
using Moq;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ServicesTest
{
    public class AepLookupServiceTest
    {

        private Mock<IAepLookupExternalService> _aepLookupExternalService = new Mock<IAepLookupExternalService>();
        private Mock<IExternalServiceFactory> _externalServiceFactory = new Mock<IExternalServiceFactory>();
        private readonly Mock<ILogger<SSTAepLookupGross>> _mockDesignEvaluationServiceLogger = new Mock<ILogger<SSTAepLookupGross>>();

        private readonly AepLookupService aepLookupService;

        public AepLookupServiceTest()
        {
            aepLookupService = new AepLookupService(_aepLookupExternalService.Object, _externalServiceFactory.Object, _mockDesignEvaluationServiceLogger.Object);
        }

        [Fact(DisplayName = "Insert New AepLookup")]
        public async Task PutAeplookupAsync_IsSuccess()
        {

            AepInputJson aepInput = new AepInputJson() { Id = 1, AEPestimateGWh = 10.4M, AEPIncrement = 46.6M, EstimationType = "baseline" };
            IList<AepInputJson> aepInputJsons = new List<AepInputJson>() { aepInput };
            SSTAepLookupGross sSTAep = new SSTAepLookupGross()
            {
                estimationType = "Baseline",
                isProposedHubHeightFound = true,
                aepNominationGross = 10.5M,
                aepInputFile = aepInputJsons,
                AepLookupUuid = Guid.NewGuid()
            };

            ExternalServiceResponse<SSTAepLookupGross> responseData = new ExternalServiceResponse<SSTAepLookupGross>()
            {
                ResponseData = sSTAep,
                IsSuccess = true
            };
            _aepLookupExternalService.Setup(x => x.PutAepAsync(It.IsAny<SSTAepLookupGross>())).ReturnsAsync((responseData));
            var result = await aepLookupService.PutAepAsync(sSTAep);

            Assert.True(result.IsSuccess);

        }

        [Fact(DisplayName = "Insert New AepLookup Failed")]
        public async Task PutAeplookupAsync_IsFailure()
        {

            AepInputJson aepInput = new AepInputJson() { Id = 1, AEPestimateGWh = 10.4M, AEPIncrement = 46.6M, EstimationType = "baseline" };
            IList<AepInputJson> aepInputJsons = new List<AepInputJson>() { aepInput };
            SSTAepLookupGross sSTAep = new SSTAepLookupGross()
            {
                estimationType = "Baseline",
                isProposedHubHeightFound = true,
                aepNominationGross = 10.5M,
                aepInputFile = aepInputJsons,
                AepLookupUuid = Guid.NewGuid()
            };

            ExternalServiceResponse<SSTAepLookupGross> responseData = new ExternalServiceResponse<SSTAepLookupGross>()
            {
                ResponseData = sSTAep,
                IsSuccess = false
            };
            _aepLookupExternalService.Setup(x => x.PutAepAsync(It.IsAny<SSTAepLookupGross>())).ReturnsAsync((responseData));
            var result = await aepLookupService.PutAepAsync(sSTAep);

            Assert.False(result.IsSuccess);

        }
    }
}
