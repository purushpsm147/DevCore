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
    public class ApplicationReasonExternalServiceTest
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();
        private Mock<ILogger<ApplicationReasonExternalService>> _mocklogger = new Mock<ILogger<ApplicationReasonExternalService>>();
        private readonly ApplicationReasonExternalService applicationReasonExternalService;
        public ApplicationReasonExternalServiceTest()
        {
            applicationReasonExternalService = new ApplicationReasonExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get all Application Reason Success")]
        public async Task GetApplicationReasonAsync_IsSuccess()
        {

            IEnumerable<ApplicationReason> data = new List<ApplicationReason>() { new ApplicationReason()
            {
                Id = 1,
                Reason = "Test1"

            },
            new ApplicationReason()
            {
                Id = 2,
                Reason= "Test2"

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

            var result = await applicationReasonExternalService.GetApplicationReasonAsync();

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all Application reason failed")]
        public async Task GetApplicationReasonAsync_IsFailure()
        {

            IEnumerable<ApplicationReason> data = new List<ApplicationReason>() { };

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

            var result = await applicationReasonExternalService.GetApplicationReasonAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all Application reason bad request")]
        public async Task GetApplicationReasonAsync_IsBadRequest()
        {

            string ex = "Some Exception";
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await applicationReasonExternalService.GetApplicationReasonAsync();

            Assert.False(result.IsSuccess);
        }
    }
}
