using Moq;
using SGRE.TSA.Api.Controllers;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Task = System.Threading.Tasks.Task;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SGRE.TSA.Test.ControllerTest
{
    public class AepLookupControllerTest
    {
        private readonly Mock<IAepLookupService> _aepLookupService = new Mock<IAepLookupService>();
        private readonly AepLookupController aepLookupController;

        public AepLookupControllerTest()
        {
            aepLookupController = new AepLookupController(_aepLookupService.Object);
        }

        [Fact(DisplayName = "Put AepLookup is Success")]
        public async Task PutAepLookupIsSuccess()
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
            _aepLookupService.Setup(x => x.PutAepAsync(It.IsAny<SSTAepLookupGross>())).ReturnsAsync((true, sSTAep));

            var result = await aepLookupController.PutAepLookup(sSTAep);
            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);

        }

        [Fact(DisplayName = "Put AepLookup is NotSuccess")]
        public async Task PutAepLookupNotSuccess()
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
            _aepLookupService.Setup(x => x.PutAepAsync(It.IsAny<SSTAepLookupGross>())).ReturnsAsync((false, sSTAep));

            var result = await aepLookupController.PutAepLookup(sSTAep);
            var badResult = new BadRequestObjectResult(result);
            Assert.Equal(400, badResult.StatusCode);

        }

    }
}
