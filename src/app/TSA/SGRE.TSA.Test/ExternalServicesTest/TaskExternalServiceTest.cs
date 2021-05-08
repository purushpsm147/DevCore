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
    public class TaskExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<TaskExternalService>> _mocklogger = new Mock<ILogger<TaskExternalService>>();

        /// <summary>
        /// The TaskExternalService
        /// </summary>
        /// <returns></returns>
        private TaskExternalService CreateTaskExternalService()
        {
            return new TaskExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName ="Get all the Roles")]
        public async Task GetRoleAsync_IsSuccess()
        {
            // Arrange
            var taskExternalService = CreateTaskExternalService();

            IEnumerable<Models.Task> data = new List<Models.Task>() { new Models.Task()
            {
                Id = 1,
                TaskName = "Task -1"
            },
            new Models.Task()
            {
                Id = 1,
                TaskName = "Task -2"
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

            var result = await taskExternalService.GetTaskAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Role No Records Found")]
        public async Task GetRoleAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var taskExternalService = CreateTaskExternalService();

            IEnumerable<Models.Task> data = new List<Models.Task>() { };

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

            var result = await taskExternalService.GetTaskAsync();

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Role Bad Request")]
        public async Task GetRoleAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var taskExternalService = CreateTaskExternalService();

            string data = "raising expection by passing plain text";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();


            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await taskExternalService.GetTaskAsync();

            Assert.False(result.IsSuccess);
        }

    }
}