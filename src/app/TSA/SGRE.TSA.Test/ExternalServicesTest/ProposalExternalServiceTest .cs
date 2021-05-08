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
    public class ProposalExternalServiceTest
    {
        /// <summary>
        /// Defines the mockHttpClientFactory
        /// </summary>
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        /// <summary>
        /// Defines the mocklogger
        /// </summary>
        private Mock<ILogger<ProposalExternalService>> _mocklogger = new Mock<ILogger<ProposalExternalService>>();

        /// <summary>
        /// The ProposalExternalService
        /// </summary>
        /// <returns></returns>
        private ProposalExternalService CreateProposalExternalService()
        {
            return new ProposalExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        }

        [Fact(DisplayName = "Get Proposal By Id")]
        public async Task GetProposalByIdAsync_IsSuccess()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
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

            var id = 1;
            var result = await proposalExternalService.GetProposalByIdAsync(id);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Proposal By Id No Records Found")]
        public async Task GetProposalByIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

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
            var result = await proposalExternalService.GetProposalByIdAsync(id);

            Assert.False(result.IsSuccess);
        }

        //[Fact(DisplayName = "Get Proposal By Id Bad Request")]
        //public async Task GetProposalByIdAsync_StateUnderTest_BadRequest()
        //{
        //    // Arrange
        //    var proposalExternalService = CreateProposalExternalService();

        //    var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        //    string ex = "some error while processing";

        //    mockHttpMessageHandler.Protected()
        //        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
        //        .Throws(new Exception(ex));

        //    var client = new HttpClient(mockHttpMessageHandler.Object);
        //    client.BaseAddress = new Uri("http://20.71.20.231/");

        //    _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

        //    var id = 1;
        //    var result = await proposalExternalService.GetProposalByIdAsync(id);

        //    Assert.False(result.IsSuccess);
        //}

        [Fact(DisplayName = "Get Proposal By Opportunity Id")]
        public async Task GetProposalByOpportunityAsync_IsSuccess()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
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

            var opportunyId = 1;
            var result = await proposalExternalService.GetProposalByOpportunityAsync(opportunyId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Proposal By Opportunity Id No Records Found")]
        public async Task GetProposalByOpportunityAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

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

            var opportunyId = 1;
            var result = await proposalExternalService.GetProposalByOpportunityAsync(opportunyId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Proposal By Opportunity Id Bad Request")]
        public async Task GetProposalByOpportunityAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var opportunyId = 1;
            var result = await proposalExternalService.GetProposalByOpportunityAsync(opportunyId);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Proposal By Opportunity Proposal Id")]
        public async Task GetProposalByOpportunityProposalIdAsync_IsSuccess()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            IEnumerable<Proposal> data = new List<Proposal>() { new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            },
            new Proposal()
            {
                Id = 2,
                ProjectId = 2,
                CertificationId = 2,
                IsMain = false,
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

            var opportunyId = 1;
            var proposalId = 1;
            var result = await proposalExternalService.GetProposalByOpportunityProposalIdAsync(opportunyId, proposalId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Proposal By Opportunity Proposal Id No Records Found")]
        public async Task GetProposalByOpportunityProposalIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

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

            var opportunyId = 1;
            var proposalId = 1;
            var result = await proposalExternalService.GetProposalByOpportunityProposalIdAsync(opportunyId, proposalId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Proposal By Opportunity Proposal Id Bad Request")]
        public async Task GetProposalByOpportunityProposalIdAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var opportunyId = 1;
            var proposalId = 1;
            var result = await proposalExternalService.GetProposalByOpportunityProposalIdAsync(opportunyId, proposalId);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Update Proposal Successfully")]
        public async Task PatchProposalAsync_IsSuccess()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            Proposal data = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
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

            var patchId = 1;
            var result = await proposalExternalService.PatchProposalAsync(patchId, data);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Proposal Failed")]
        public async Task PatchProposalAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            Proposal data = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
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

            var patchId = 1;
            var result = await proposalExternalService.PatchProposalAsync(patchId, data);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Update Proposal Failed Bad Request/Exception")]
        public async Task PatchProposalAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            Proposal data = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

            var patchId = 1;
            var result = await proposalExternalService.PatchProposalAsync(patchId, data);

            Assert.False(result.IsSuccess);
        }



        [Fact(DisplayName = "Insert New Proposal Successfully")]
        public async Task PutProposalAsync_IsSuccess()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            Proposal data = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
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

            var result = await proposalExternalService.PutProposalAsync(data);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Insert New Proposal Failed")]
        public async Task PutProposalAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            Proposal data = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
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

            var result = await proposalExternalService.PutProposalAsync(data);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert New Proposal Failed Bad Request/Exception")]
        public async Task PutProposalAsync_StateUnderTest_BadRequest()
        {
            // Arrange
            var proposalExternalService = CreateProposalExternalService();

            Proposal data = new Proposal()
            {
                Id = 1,
                ProjectId = 1,
                CertificationId = 1,
                IsMain = true,
                RecordInsertDateTime = DateTime.UtcNow,
                LastModifiedDateTime = DateTime.UtcNow
            };


            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            string ex = "some error while processing";

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Throws(new Exception(ex));

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri("http://20.71.20.231/");

            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

           var result = await proposalExternalService.PutProposalAsync(data);

            Assert.False(result.IsSuccess);
        }
    }
}