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
    public class DESummaryExternalServiceTest
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        //private Mock<ILogger<DesignEvaluationExternalService>> _mocklogger = new Mock<ILogger<DesignEvaluationExternalService>>();

        //private readonly IDesignEvaluationExternalService dESummaryExternalService;
        //public DESummaryExternalServiceTest()
        //{
        //    dESummaryExternalService = new DesignEvaluationExternalService(_mockHttpClientFactory.Object, _mocklogger.Object);
        //}

        //[Fact(DisplayName = "Get Summary By ID is Succcess")]
        //public async Task GetSummaryByIdAsync_IsSuccess()
        //{
        //    // Arrange

        //    IEnumerable<Summary> data = new List<Summary>() { new Summary()
        //    {
        //        ClusterName = "test",
        //        DesignLifetime = 10,
        //        InitialTower = "tdg",
        //        QuotationId = "test",
        //        sstTargetLifetime = 10,
        //        TurbineQuantity = 10,
        //        WtgType = "ysuu"
        //    } };

        //    string payload = JsonConvert.SerializeObject(data);

        //    var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        //    mockHttpMessageHandler.Protected()
        //        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
        //        .ReturnsAsync(new HttpResponseMessage
        //        {
        //            StatusCode = HttpStatusCode.OK,
        //            Content = new StringContent(payload, Encoding.UTF8, "application/json"),
        //        });

        //    var client = new HttpClient(mockHttpMessageHandler.Object);
        //    client.BaseAddress = new Uri("http://20.71.20.231/");

        //    _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

        //    var sSSInput = 1;
        //    var result = await dESummaryExternalService.GetSummaryByIdAsync(sSSInput);

        //    Assert.True(result.IsSuccess);
        //}

        //[Fact(DisplayName = "Get Summary By ID is Failure")]
        //public async Task GetSummaryByIdAsync_IsFailure()
        //{
        //    // Arrange

        //    IEnumerable<Summary> data = new List<Summary>() { new Summary()
        //    {
               
        //    } };

        //    string payload = JsonConvert.SerializeObject(data);

        //    var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        //    mockHttpMessageHandler.Protected()
        //        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
        //        .ReturnsAsync(new HttpResponseMessage
        //        {
        //            StatusCode = HttpStatusCode.NotFound,
        //            Content = new StringContent(payload, Encoding.UTF8, "application/json"),
        //        });

        //    var client = new HttpClient(mockHttpMessageHandler.Object);
        //    client.BaseAddress = new Uri("http://20.71.20.231/");

        //    _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();

        //    var sSSInput = 1;
        //    var result = await dESummaryExternalService.GetSummaryByIdAsync(sSSInput);

        //    Assert.False(result.IsSuccess);
        //}
    }
}
