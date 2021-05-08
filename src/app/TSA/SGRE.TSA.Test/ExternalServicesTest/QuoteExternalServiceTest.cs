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
    public class QuoteExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<QuoteExternalService>> _mocklogger = new Mock<ILogger<QuoteExternalService>>();

        /// <summary>
        /// The QuoteExternalService
        /// </summary>
        /// <returns></returns>
        private QuoteExternalService CreateQuoteExternalService()
        {
            return new QuoteExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get all the Quotes")]
        public async Task GetQuotesAsync_IsSuccess()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

            IEnumerable<Quote> data = new List<Quote>() { new Quote()
            {
                Id = 1,
                ProposalId  = 1,
                QuotationName = "Quote",
                //OfferType = true,
                QuoteLines = new List<QuoteLine>() { new QuoteLine(){ Id = 1, Quantity = 1} }
            },
            new Quote()
            {
                Id = 2,
                ProposalId  = 1,
                QuotationName = "Quote",
                //OfferType = false,
                QuoteLines = new List<QuoteLine>() { new QuoteLine(){ Id = 1, Quantity = 1} }
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

            int proposalId = 1;
            var result = await quoteExternalService.GetQuotesAsync(proposalId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Quotes No Records Found")]
        public async Task GetQuotesAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

            IEnumerable<Quote> data = new List<Quote>() { };

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

            int proposalId = 1;
            var result = await quoteExternalService.GetQuotesAsync(proposalId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the Quotes Bad Request")]
        public async Task GetQuotesAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

            string data = "raising expection by passing plain text";

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();


            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            int proposalId = 1;
            var result = await quoteExternalService.GetQuotesAsync(proposalId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Quotes By id")]
        public async Task GetQuotesByIdAsync_IsSuccess()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

            IEnumerable<Quote> data = new List<Quote>() { new Quote()
            {
                Id = 1,
                ProposalId  = 1,
                QuotationName = "Quote",
                QuoteLines = new List<QuoteLine>() { new QuoteLine(){ Id = 1, Quantity = 1} }
            },
            new Quote()
            {
                Id = 2,
                ProposalId  = 1,
                QuotationName = "Quote",
                QuoteLines = new List<QuoteLine>() { new QuoteLine(){ Id = 1, Quantity = 1} }
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


            var id = 1;
            var result = await quoteExternalService.GetQuotesAsync(id);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Quote By Id No Records Found")]
        public async Task GetQuoteByIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

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

            var id = 1;
            var result = await quoteExternalService.GetQuotesAsync(id);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Quote By Id Bad Request/RaiseException")]
        public async Task GetQuoteByIdAsync_StateUnderTest_RaiseException()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var id = 1;
            var result = await quoteExternalService.GetQuotesByIdAsync(id);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Put Quote")]
        public async Task PutRegionsAsync_IsSuccess()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

            var data = new Quote()
            {
                Id = 1,
                ProposalId = 1,
                QuotationName = "Quote",
                QuoteLines = new List<QuoteLine>() { new QuoteLine() { Id = 1, Quantity = 1 } }
            };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();


            var result = await quoteExternalService.PutQuoteAsync(data);

            Assert.Equal(result.ResponseData,data);
        }


        [Fact(DisplayName = "Put Quote Bad Request")]
        public async Task PutQuoteAsync_IsFailed()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

            var data = new Quote()
            {
                Id = 1,
                ProposalId = 1,
                QuotationName = "Quote",
                QuoteLines = new List<QuoteLine>() { new QuoteLine() { Id = 1, Quantity = 1 } }
            };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();



            var result = await quoteExternalService.PutQuoteAsync(data);

            Assert.Null(result.ResponseData);
        }

        [Fact(DisplayName = "Put Quote Raise Exception")]
        public async Task PutQuoteAsync_IsFailed_RaiseException()
        {
            // Arrange
            var quoteExternalService = CreateQuoteExternalService();

            var data = new Quote()
            {
                Id = 1,
                ProposalId = 1,
                QuotationName = "Quote",
                QuoteLines = new List<QuoteLine>() { new QuoteLine() { Id = 1, Quantity = 1 } }
            };

            string payload = JsonConvert.SerializeObject(data);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();


            var result = await quoteExternalService.PutQuoteAsync(data);

            Assert.Null(result.ResponseData);
        }

    }
}