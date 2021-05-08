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
    public class CountryExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<CountryExternalService>> _mocklogger = new Mock<ILogger<CountryExternalService>>();

        /// <summary>
        /// The RegionExternalService
        /// </summary>
        /// <returns></returns>
        private CountryExternalService CreateCountryExternalService()
        {
            return new CountryExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get all the Certifications")]
        public async Task GetCertificationAsync_IsSuccess()
        {
            // Arrange
            var countryExternalService = CreateCountryExternalService();

            IEnumerable<Country> data = new List<Country>() { new Country()
            {
                Id = 1,
                CountryName = "Certification - 1"
            },
            new Country()
            {
                Id = 2,
                CountryName = "Certification - 2"
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

            var result = await countryExternalService.GetCountryAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Certifications No Records Found")]
        public async Task GetCertificationAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            // Arrange
            var countryExternalService = CreateCountryExternalService();

            IEnumerable<Country> data = new List<Country>() { };

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

            var result = await countryExternalService.GetCountryAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Certification Bad Request")]
        public async Task GetCertificationAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var countryExternalService = CreateCountryExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var result = await countryExternalService.GetCountryAsync();

            Assert.False(result.IsSuccess);
        }

    }
}