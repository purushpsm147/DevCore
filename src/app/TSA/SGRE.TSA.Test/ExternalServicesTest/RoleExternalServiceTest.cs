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
    public class RoleExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<RoleExternalService>> _mocklogger = new Mock<ILogger<RoleExternalService>>();

        /// <summary>
        /// The RoleExternalService
        /// </summary>
        /// <returns></returns>
        private RoleExternalService CreateRoleExternalService()
        {
            return new RoleExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName ="Get all the Roles")]
        public async Task GetRoleAsync_IsSuccess()
        {
            // Arrange
            var roleExternalService = CreateRoleExternalService();

            IEnumerable<Role> data = new List<Role>() { new Role()
            {
                Id = 1,
                RoleName = "Engineer"
            },
            new Role()
            {
                Id = 2,
                RoleName = "Service Engineer"
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

            var result = await roleExternalService.GetRoleAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Role No Records Found")]
        public async Task GetRoleAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var roleExternalService = CreateRoleExternalService();

            IEnumerable<Role> data = new List<Role>() { };

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

            var result = await roleExternalService.GetRoleAsync();

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Role Bad Request")]
        public async Task GetRoleAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var roleExternalService = CreateRoleExternalService();

            string data = "raising expection by passing plain text";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();


            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await roleExternalService.GetRoleAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName ="Get all Preset Roles")]
        public async Task GetPresetRoleAsync_IsSuccess()
        {
            // Arrange
            var roleExternalService = CreateRoleExternalService();

            IEnumerable<PresetRoles> data = new List<PresetRoles>() { new PresetRoles()
            {
                 Id =1,
                 RegionId = 1,
                 UserName = "Manager"

            },
            new PresetRoles() {
                 Id =2,
                 RegionId = 2,
                 UserName = "Employee"
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

            var result = await roleExternalService.GetPresetRolesAsync();

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName ="Get all the Preset Role No Record found")]
        public async Task GetPresetRoleAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var roleExternalService = CreateRoleExternalService();

            IEnumerable<PresetRoles> data = new List<PresetRoles>() { };

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

            var result = await roleExternalService.GetPresetRolesAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the PresetRole Bad Request")]
        public async Task GetPresetRoleAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var roleExternalService = CreateRoleExternalService();

            string data = "raising expection by passing plain text";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();


            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await roleExternalService.GetPresetRolesAsync();

            Assert.False(result.IsSuccess);
        }
    }
}