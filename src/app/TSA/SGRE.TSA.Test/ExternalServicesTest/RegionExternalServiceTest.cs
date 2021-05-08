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
    public class RegionExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<RegionExternalService>> _mocklogger = new Mock<ILogger<RegionExternalService>>();

        /// <summary>
        /// The RegionExternalService
        /// </summary>
        /// <returns></returns>
        private RegionExternalService CreateRegionExternalService()
        {
            return new RegionExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get all the Regions")]
        public async Task GetRegionsAsync_IsSuccess()
        {
            // Arrange
            var regionExternalService = CreateRegionExternalService();

            IEnumerable<Region> data = new List<Region>() { new Region()
            {
                Id = 1,
                RegionName = "USA"
            },
            new Region()
            {
                Id = 2,
                RegionName = "UK"
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

            var result = await regionExternalService.GetRegionsAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Regions No Records Found")]
        public async Task GetRegionsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var regionExternalService = CreateRegionExternalService();

            IEnumerable<Region> data = new List<Region>() { };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent("Empty String", Encoding.UTF8, "application/json"),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await regionExternalService.GetRegionsAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Regions Bad Request")]
        public async Task GetRegionsAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var regionExternalService = CreateRegionExternalService();

            string data = "raising expection by passing plain text";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();


            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await regionExternalService.GetRegionsAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Regions By id")]
        public async Task GetRegionsByIdAsync_IsSuccess()
        {
            // Arrange
            var regionExternalService = CreateRegionExternalService();

            IEnumerable<Region> data = new List<Region>() { new Region()
            {
                Id = 1,
                RegionName = "USA"
            },
            new Region()
            {
                Id = 2,
                RegionName = "UK"
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


            var id = 1;
            var result = await regionExternalService.GetRegionsAsync(id);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Regions By Id No Records Found")]
        public async Task GetRegionsByIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var regionExternalService = CreateRegionExternalService();

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

            var id = 1;
            var result = await regionExternalService.GetRegionsAsync(id);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Regions By Id Bad Request/RaiseException")]
        public async Task GetRegionsByIdAsync_StateUnderTest_RaiseException()
        {
            // Arrange
            var regionExternalService = CreateRegionExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var id = 1;
            var result = await regionExternalService.GetRegionsAsync(id);

            Assert.False(result.IsSuccess);
        }


    }
}