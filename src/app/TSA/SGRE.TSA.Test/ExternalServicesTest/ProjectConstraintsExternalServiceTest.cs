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
    public class ProjectConstraintsExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<ProjectConstraintsExternalService>> _mocklogger = new Mock<ILogger<ProjectConstraintsExternalService>>();

        /// <summary>
        /// The ProjectConstraintsExternalService
        /// </summary>
        /// <returns></returns>
        private ProjectConstraintsExternalService CreateProjectConstraintsExternalService()
        {
            return new ProjectConstraintsExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get Project Constraints By Project Id")]
        public async Task GetProjectConstraintsAsync_IsSuccess()
        {
            // Arrange
            var projectConstraintsExternalService = CreateProjectConstraintsExternalService();

            IEnumerable<ProjectConstraint> data = new List<ProjectConstraint>() { new ProjectConstraint()
            {
                Id = 1,
                ProjectId =1,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new ProjectConstraint()
            {
                Id = 2,
                ProjectId =1,
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

            var projectId = 1;
            var result = await projectConstraintsExternalService.GetProjectConstraintsAsync(projectId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Project Constraints By Project No Records Found")]
        public async Task GetProjectConstraintsAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var projectConstraintsExternalService = CreateProjectConstraintsExternalService();

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

            var projectId = 1;
            var result = await projectConstraintsExternalService.GetProjectConstraintsAsync(projectId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Project Constraints By Project Bad Request")]
        public async Task GetProjectConstraintsAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var projectConstraintsExternalService = CreateProjectConstraintsExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var projectId = 1;
            var result = await projectConstraintsExternalService.GetProjectConstraintsAsync(projectId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert New Project Constraint Successfully")]
        public async Task PutProjectConstraintsAsync_IsSuccess()
        {
            // Arrange
            var projectConstraintsExternalService = CreateProjectConstraintsExternalService();

            ProjectConstraint data =  new ProjectConstraint()
            {
                Id = 1,
                ProjectId =1,
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

            var result = await projectConstraintsExternalService.PutProjectConstraintsAsync(data);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Insert New Scenario Failed")]
        public async Task PutScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var projectConstraintsExternalService = CreateProjectConstraintsExternalService();

            ProjectConstraint data = new ProjectConstraint()
            {
                Id = 1,
                ProjectId = 1,
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

            var result = await projectConstraintsExternalService.PutProjectConstraintsAsync(data);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert New Scenario Failed Bad Request/Exception")]
        public async Task PutScenarioAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var projectConstraintsExternalService = CreateProjectConstraintsExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            ProjectConstraint data = new ProjectConstraint()
            {
                Id = 1,
                ProjectId = 1,
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

            var result = await projectConstraintsExternalService.PutProjectConstraintsAsync(data);

            Assert.False(result.IsSuccess);
        }
    }
}