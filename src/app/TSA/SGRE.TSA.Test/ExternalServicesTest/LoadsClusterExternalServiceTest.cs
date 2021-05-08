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
    public class LoadsClusterExternalServiceTest
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        private Mock<ILogger<LoadsClusterExternalService>> _mocklogger = new Mock<ILogger<LoadsClusterExternalService>>();
        private readonly LoadsClusterExternalService loadsClusterExternalService;

        public LoadsClusterExternalServiceTest()
        {
            loadsClusterExternalService = new LoadsClusterExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get all the Load Clusters Success")]
        public async Task GetLoadsClusterAsync_IsSuccess()
        {

            IEnumerable<LoadsCluster> data = new List<LoadsCluster>() { new LoadsCluster()
            {
                Id = 1,
                ClusterName = "Test1"

            },
            new LoadsCluster()
            {
                Id = 2,
                ClusterName = "Test2"

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

            var result = await loadsClusterExternalService.GetLoadsClusterAsync();

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Load Clusters failed")]
        public async Task GetLoadsClusterAsync_IsFailure()
        {

            IEnumerable<LoadsCluster> data = new List<LoadsCluster>() { };

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

            var result = await loadsClusterExternalService.GetLoadsClusterAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Load Clusters bad request")]
        public async Task GetLoadsClusterAsync_IsBadRequest()
        {

            string ex = "Some Exception";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await loadsClusterExternalService.GetLoadsClusterAsync();

            Assert.False(result.IsSuccess);
        }
    }
}
