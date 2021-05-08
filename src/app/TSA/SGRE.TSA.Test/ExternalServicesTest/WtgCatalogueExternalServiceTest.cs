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
    public class WtgCatalogueExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<WtgCatalogueExternalService>> _mocklogger = new Mock<ILogger<WtgCatalogueExternalService>>();

        /// <summary>
        /// The WtgCatalogueExternalService
        /// </summary>
        /// <returns></returns>
        private WtgCatalogueExternalService CreateWtgCatalogueExternalService()
        {
            return new WtgCatalogueExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get all the WtgCatalogue")]
        public async Task GetWtgCatalogueAsync_IsSuccess()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

            IEnumerable<WtgCatalogue> data = new List<WtgCatalogue>() { new WtgCatalogue()
            {
                Id = 1,
                WtgSizeMW = 1
            },
            new WtgCatalogue()
            {
                Id = 2,
                WtgSizeMW = 2
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

            var result = await wtgCatalogueExternalService.GetWtgCatalogueAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Regions No Records Found")]
        public async Task GetWtgCatalogueAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

            IEnumerable<WtgCatalogue> data = new List<WtgCatalogue>() { };

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


            var result = await wtgCatalogueExternalService.GetWtgCatalogueAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Regions Bad Request")]
        public async Task GetWtgCatalogueAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

            string data = "raising expection by passing plain text";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();


            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await wtgCatalogueExternalService.GetWtgCatalogueAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get WtgCatalogue By id")]
        public async Task GetWtgCatalogueByIdAsync_IsSuccess()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

            IEnumerable<WtgCatalogue> data = new List<WtgCatalogue>() { new WtgCatalogue()
            {
                Id = 1,
                WtgSizeMW = 1
            },
            new WtgCatalogue()
            {
                Id = 2,
                WtgSizeMW = 2
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
            var result = await wtgCatalogueExternalService.GetWtgCatalogueAsync(id);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get WtgCatalogue By Id No Records Found")]
        public async Task GetWtgCatalogueByIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

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
            var result = await wtgCatalogueExternalService.GetWtgCatalogueAsync(id);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get WtgCatalogue By Id Bad Request/RaiseException")]
        public async Task GetWtgCatalogueByIdAsync_StateUnderTest_RaiseException()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var id = 1;
            var result = await wtgCatalogueExternalService.GetWtgCatalogueAsync(id);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the WtgThreshold")]
        public async Task GetWtgThresholdAsync_IsSuccess()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

            IEnumerable<WtgThreshold> data = new List<WtgThreshold>() { new WtgThreshold()
            {
                Id = 1,
                WindFarmSize = 10
            },
            new WtgThreshold()
            {
                Id = 2,
                WindFarmSize = 20
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

            var result = await wtgCatalogueExternalService.GetWtgThresholdAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the WtgThreshold No Records Found")]
        public async Task GetWtgThresholdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

            IEnumerable<WtgThreshold> data = new List<WtgThreshold>() { };

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


            var result = await wtgCatalogueExternalService.GetWtgThresholdAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the WtgThreshold Bad Request")]
        public async Task GetWtgThresholdAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var wtgCatalogueExternalService = CreateWtgCatalogueExternalService();

            string data = "raising expection by passing plain text";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();


            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await wtgCatalogueExternalService.GetWtgThresholdAsync();

            Assert.False(result.IsSuccess);
        }


    }
}