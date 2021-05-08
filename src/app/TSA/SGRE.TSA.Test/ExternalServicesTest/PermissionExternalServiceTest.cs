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
    public class PermissionExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<PermissionExternalService>> _mocklogger = new Mock<ILogger<PermissionExternalService>>();

        /// <summary>
        /// The PermissionExternalService
        /// </summary>
        /// <returns></returns>
        private PermissionExternalService CreatePermissionExternalService()
        {
            return new PermissionExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get All MileStones")]
        public async Task GetPermissionAsync_IsSuccess()
        {
            // Arrange
            var permissionExternalService = CreatePermissionExternalService();

            IEnumerable<Permission> data = new List<Permission>() { new Permission()
            {
                Id = 1,
                RoleId = 1,
                CreatedBy = "user - 1",
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime= DateTime.UtcNow,

            },
            new Permission()
            {
                Id = 2,
                CreatedBy = "user - 2",
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

            var roleId = 1;
            var result = await permissionExternalService.GetPermissionAsync(roleId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get All MileStones No Records Found")]
        public async Task GetPermissionAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var permissionExternalService = CreatePermissionExternalService();


            IEnumerable<Permission> data = new List<Permission>() { };

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

            var roleId = 1;
            var result = await permissionExternalService.GetPermissionAsync(roleId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get All MileStones Bad Request/Ecxeption")]
        public async Task GetPermissionAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var permissionExternalService = CreatePermissionExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var roleId = 1;
            var result = await permissionExternalService.GetPermissionAsync(roleId);

            Assert.False(result.IsSuccess);
        }

    }
}