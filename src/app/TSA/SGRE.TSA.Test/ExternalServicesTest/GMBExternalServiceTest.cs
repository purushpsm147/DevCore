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
    public class GMBExternalServiceTest
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        private Mock<ILogger<GMBExternalService>> _mocklogger = new Mock<ILogger<GMBExternalService>>();
        private readonly GMBExternalService gMBExternalService;

        public GMBExternalServiceTest()
        {
            gMBExternalService = new GMBExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get all the Generic Market Boundaries Success")]
        public async Task GetGMBAsync_IsSuccess()
        {

            IEnumerable<GenericMarketBoundary> data = new List<GenericMarketBoundary>() { new GenericMarketBoundary()
            {
                Id = 1,
                CountryId = 1,
                TransportModeId = 1,
                MaxSegmentLengthMtrs = 1,
                MaxSegmentWeightTon = 1,
                MaxTowerBaseDiameterMtrs = 1
            },
            new GenericMarketBoundary()
            {
                Id = 2,
                CountryId = 2,
                TransportModeId = 2,
                MaxSegmentLengthMtrs = 2,
                MaxSegmentWeightTon = 2,
                MaxTowerBaseDiameterMtrs = 2
            }};

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await gMBExternalService.GetGMBAsync(1);

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Generic Market Boundaries failed")]
        public async Task GetGMBAsync_IsFailure()
        {

            IEnumerable<GenericMarketBoundary> data = new List<GenericMarketBoundary>() { };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await gMBExternalService.GetGMBAsync(1);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Generic Market Boundaries bad request")]
        public async Task GetGMBAsync_IsBadRequest()
        {

            string ex = "Some Exception";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await gMBExternalService.GetGMBAsync(1);

            Assert.False(result.IsSuccess);
        }


    }
}
