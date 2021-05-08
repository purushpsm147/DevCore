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
    public class MileStonesExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<MileStonesExternalService>> _mocklogger = new Mock<ILogger<MileStonesExternalService>>();

        /// <summary>
        /// The RegionExternalService
        /// </summary>
        /// <returns></returns>
        private MileStonesExternalService CreateMileStonesExternalService()
        {
            return new MileStonesExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get All MileStones")]
        public async Task GetMileStonesAsync_IsSuccess()
        {
            // Arrange
            var mileStonesExternalService = CreateMileStonesExternalService();

            IEnumerable<MileStone> data = new List<MileStone>() { new MileStone()
            {
                Id = 1,
                MileStoneName = "Milstone - 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,

            },
            new MileStone()
            {
                Id = 2,
                MileStoneName = "Milstone - 2",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,
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

            var result = await mileStonesExternalService.GetMileStonesAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get All MileStones No Records Found")]
        public async Task GetMileStonesAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var mileStonesExternalService = CreateMileStonesExternalService();

            IEnumerable<MileStone> data = new List<MileStone>() {  };

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

            var result = await mileStonesExternalService.GetMileStonesAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All MileStones Bad Request/Ecxeption")]
        public async Task GetMileStonesAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var mileStonesExternalService = CreateMileStonesExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await mileStonesExternalService.GetMileStonesAsync();

            Assert.False(result.IsSuccess);
        }

    }
}