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
    public class ConfigScenarioExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<ConfigScenarioExternalService>> _mocklogger = new Mock<ILogger<ConfigScenarioExternalService>>();

        /// <summary>
        /// The RegionExternalService
        /// </summary>
        /// <returns></returns>
        private ConfigScenarioExternalService CreateConfigScenarioExternalService()
        {
            return new ConfigScenarioExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get Cost KPI Scenario By config Id")]
        public async Task GetCostKPIScenarioAsync_IsSuccess()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            IEnumerable<Scenario> data = new List<Scenario>() { new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Scenario()
            {
                Id = 2,
                ScenarioNo = 2,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
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

            var configId = "1";
            var result = await configScenarioExternalService.GetScenarioAsync(configId, 1);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Cost KPIScenario No Records Found")]
        public async Task GetCostKPIScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

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

            var configId = "1";
            var result = await configScenarioExternalService.GetScenarioAsync(configId, 1);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Regions Bad Request")]
        public async Task GetRegionsAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();


            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var configId = "1";
            var result = await configScenarioExternalService.GetScenarioAsync(configId, 1);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Get Design Scenario By config Id")]
        public async Task GetDesignScenarioAsync_IsSuccess()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            IEnumerable<Scenario> data = new List<Scenario>() { new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Scenario()
            {
                Id = 2,
                ScenarioNo = 2,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
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

            var configId = "1";
            var result = await configScenarioExternalService.GetScenarioAsync(configId, 1);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Design Scenario  No Records Found")]
        public async Task GetDesignScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

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

            var configId = "1";
            var result = await configScenarioExternalService.GetScenarioAsync(configId, 1);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Design Scenario Bad Request")]
        public async Task GetDesignScenarioAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var configId = "1";
            var result = await configScenarioExternalService.GetScenarioAsync(configId, 1);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Scenario Successfully")]
        public async Task PatchScenarioAsync_IsSuccess()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            Scenario data = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

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

            var configId = 1;
            var result = await configScenarioExternalService.PatchScenarioAsync(configId, data);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Scenario Failed")]
        public async Task PatchScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            Scenario data = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

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

            var configId = 1;
            var result = await configScenarioExternalService.PatchScenarioAsync(configId, data);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Update Scenario Failed Bad Request/Exception")]
        public async Task PatchScenarioAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            Scenario data = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var configId = 1;
            var result = await configScenarioExternalService.PatchScenarioAsync(configId, data);

            Assert.False(result.IsSuccess);
        }



        [Fact(DisplayName = "Insert New Scenario Successfully")]
        public async Task PutScenarioAsync_IsSuccess()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            Scenario data = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow,
            };

            IEnumerable<Scenario> ScenarioData = new List<Scenario>() { new Scenario()
            {
                Id = 1,
                ScenarioNo = 1
            },
            new Scenario()
            {
                Id = 2,
                ScenarioNo = 2
            }};

            string payload = JsonConvert.SerializeObject(ScenarioData);

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

            var result = await configScenarioExternalService.PutScenarioAsync(data);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Insert New Scenario Failed")]
        public async Task PutScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            Scenario data = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

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

            var result = await configScenarioExternalService.PutScenarioAsync(data);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert New Scenario Failed Bad Request/Exception")]
        public async Task PutScenarioAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var configScenarioExternalService = CreateConfigScenarioExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            Scenario data = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

           var result = await configScenarioExternalService.PutScenarioAsync(data);

            Assert.False(result.IsSuccess);
        }
    }
}