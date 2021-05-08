using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ExternalServicesTest
{
    public class AepLookupExternalServiceTest
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        private Mock<ILogger<AepLookupExternalService>> _mocklogger = new Mock<ILogger<AepLookupExternalService>>();
        private readonly AepLookupExternalService aepLookupExternal;

        public AepLookupExternalServiceTest()
        {
            aepLookupExternal = new AepLookupExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);

        }

        [Fact(DisplayName = "Put AepLookUp Success")]
        public async Task PutAepAsync_IsSuccess()
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

            string payload = JsonConvert.SerializeObject(sSTAep);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();


            var result = await aepLookupExternal.PutAepAsync(sSTAep);

            Assert.Equal(result.ResponseData, sSTAep);
        }

        [Fact(DisplayName = "Put AepLookUp BadRequest")]
        public async Task PutAepAsync_BadRequest()
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

            string payload = JsonConvert.SerializeObject(sSTAep);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();


            var result = await aepLookupExternal.PutAepAsync(sSTAep);
            var badResult = new BadRequestObjectResult(result);
            Assert.Equal(400, badResult.StatusCode);
        }

        [Fact(DisplayName = "Put AepLookUp Exception Occured")]
        public async Task PutAepAsync_Exception()
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

            string payload = JsonConvert.SerializeObject(sSTAep);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();


            var result = await aepLookupExternal.PutAepAsync(sSTAep);
            Assert.Null(result.ResponseData);
        }
    }
}
