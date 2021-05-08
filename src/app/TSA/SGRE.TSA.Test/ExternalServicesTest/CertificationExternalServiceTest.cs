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
    public class CertificationExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<CertificationExternalService>> _mocklogger = new Mock<ILogger<CertificationExternalService>>();

        /// <summary>
        /// The RegionExternalService
        /// </summary>
        /// <returns></returns>
        private CertificationExternalService CreateCertificationExternalService()
        {
            return new CertificationExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get all the Certifications")]
        public async Task GetCertificationAsync_IsSuccess()
        {
            // Arrange
            var certificationExternalService = CreateCertificationExternalService();

            IEnumerable<Certification> data = new List<Certification>() { new Certification()
            {
                Id = 1,
                CertificationName = "Certification - 1"
            },
            new Certification()
            {
                Id = 2,
                CertificationName = "Certification - 2"
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

            var result = await certificationExternalService.GetCertificationAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Certifications No Records Found")]
        public async Task GetCertificationAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var certificationExternalService = CreateCertificationExternalService();

            IEnumerable<Certification> data = new List<Certification>() { };

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

            var result = await certificationExternalService.GetCertificationAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Certification Bad Request")]
        public async Task GetCertificationAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var certificationExternalService = CreateCertificationExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await certificationExternalService.GetCertificationAsync();

            Assert.False(result.IsSuccess);
        }

    }
}