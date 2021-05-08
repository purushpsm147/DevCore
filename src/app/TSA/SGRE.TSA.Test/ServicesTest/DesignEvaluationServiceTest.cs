using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ServicesTest
{
    public class DesignEvaluationServiceTest
    {

        private readonly Mock<IExternalServiceFactory> mockServiceFactory = new Mock<IExternalServiceFactory>();
        private readonly Mock<ILogger<DesignEvaluationService>> logger = new Mock<ILogger<DesignEvaluationService>>();
        private readonly Mock<IConfiguration> configMock = new Mock<IConfiguration>();

        private DesignEvaluationService CreateDesignEvaluationService()
        {
            return new DesignEvaluationService(mockServiceFactory.Object,  logger.Object, configMock.Object);
        }

        [Fact(DisplayName = "Get all DesignResult IsSuccess")]
        public async Task GetrequestDesignResultIsSuccess()
        {
            var _designService = CreateDesignEvaluationService();

            IEnumerable<RequestDesignEvaluation> requestDesigns = new List<RequestDesignEvaluation>() { new RequestDesignEvaluation()
            {
                 DesignAuthor="Anilananda"
            } };

            ExternalServiceResponse<IEnumerable<RequestDesignEvaluation>> responseData = new ExternalServiceResponse<IEnumerable<RequestDesignEvaluation>>()
            {
                ResponseData = requestDesigns,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<RequestDesignEvaluation>(logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var sstTowerId = 1;
            var result = await _designService.GetRequestDesignAsync(sstTowerId);

            Assert.True(result.IsSuccess);
            Assert.Equal(result.requestDesignResult.ToString(), requestDesigns.ToString());
            Assert.IsType<List<RequestDesignEvaluation>>(result.requestDesignResult);
        }


        [Fact(DisplayName = "Get all the Base Tower Id No Records Found")]
        public async Task GetrequestDesignResult_UnExpectedBehavior()
        {
            var _designService = CreateDesignEvaluationService();

            ExternalServiceResponse<IEnumerable<RequestDesignEvaluation>> responseData = new ExternalServiceResponse<IEnumerable<RequestDesignEvaluation>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<RequestDesignEvaluation>(logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var sstTowerId = 1;
            var result = await _designService.GetRequestDesignAsync(sstTowerId);

            Assert.False(result.IsSuccess);
            Assert.Null(result.requestDesignResult);
        }


        [Fact(DisplayName = "Put Design Evaluation is success")]
        public async Task PutDesignEvaluationAsync_IsSuccess()
        {
            var _designService = CreateDesignEvaluationService();

            SstDesignEvaluation sstDesignEvaluation = new SstDesignEvaluation()
            {
                sstTowerId = 96,
                DesignLifetimeStatus = "ELSA/SAFAL dataset and SST input target lifetime is not consistent"
            };

            ExternalServiceResponse<SstDesignEvaluation> SstDesignEvaluationData = new ExternalServiceResponse<SstDesignEvaluation>()
            {
                ResponseData = sstDesignEvaluation,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstDesignEvaluation>(logger.Object).PutAsync(It.IsAny<SstDesignEvaluation>())).ReturnsAsync((SstDesignEvaluationData));

            var result = await _designService.PutDesignEvaluationAsync(sstDesignEvaluation);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Put Design Evaluation No Records Found")]
        public async Task PutDesignEvaluation_UnExpectedBehavior()
        {
            var _designService = CreateDesignEvaluationService();

            SstDesignEvaluation sstDesignEvaluation = new SstDesignEvaluation()
            {
                sstTowerId = 0,
                DesignLifetimeStatus = "ELSA/SAFAL dataset and SST input target lifetime is not consistent"
            };

            ExternalServiceResponse<SstDesignEvaluation> responseData = new ExternalServiceResponse<SstDesignEvaluation>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstDesignEvaluation>(logger.Object).PutAsync(It.IsAny<SstDesignEvaluation>())).ReturnsAsync((responseData));

            var result = await _designService.PutDesignEvaluationAsync(sstDesignEvaluation);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Update DesignEvaluation")]
        public async Task PatchDesignEvaluationAsync_IsSuccess()
        {

            var _designService = CreateDesignEvaluationService();

            SstDesignEvaluation sstDesignEvaluation = new SstDesignEvaluation()
            {
                sstTowerId = 96,
                DesignLifetimeStatus = "ELSA/SAFAL dataset and SST input target lifetime is not consistent"
            };


            ExternalServiceResponse<SstDesignEvaluation> responseData = new ExternalServiceResponse<SstDesignEvaluation>()
            {
                ResponseData = sstDesignEvaluation,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstDesignEvaluation>(logger.Object).PatchAsync(It.IsAny<SstDesignEvaluation>())).ReturnsAsync((responseData));

            var result = await _designService.PatchDesignEvaluationAsync(sstDesignEvaluation);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Update DesignEvaluation No Records Found")]
        public async Task PatchDesignEvaluationAsync_UnExpectedBehavior()
        {

            var _designService = CreateDesignEvaluationService();

            SstDesignEvaluation sstDesignEvaluation = new SstDesignEvaluation()
            {
                sstTowerId = 0,
                DesignLifetimeStatus = "ELSA/SAFAL dataset and SST input target lifetime is not consistent"
            };

            ExternalServiceResponse<SstDesignEvaluation> responseData = new ExternalServiceResponse<SstDesignEvaluation>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstDesignEvaluation>(logger.Object).PatchAsync(It.IsAny<SstDesignEvaluation>())).ReturnsAsync((responseData));

            var result = await _designService.PatchDesignEvaluationAsync(sstDesignEvaluation);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Design Evaluaion segment Summary By Id is Success")]
        public async Task GetSegmentSummaryIdAsync_IsSuccess()
        {
            var _designService = CreateDesignEvaluationService();
            IEnumerable<SegmentDimensionSummary> data = new List<SegmentDimensionSummary>() { new SegmentDimensionSummary()
               {
                 HubHeight=9999,
                 NumberOfSections=3,
                 SstTowerId=1,
                 TowerHeight=999
                }
            };

            ExternalServiceResponse<IEnumerable<SegmentDimensionSummary>> responseData = new ExternalServiceResponse<IEnumerable<SegmentDimensionSummary>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SegmentDimensionSummary>(logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var sstTowerId = 1;
            var result = await _designService.GetSegmentDimensionSummaryAsync(sstTowerId);

            Assert.True(result.IsSuccess);

            Assert.Equal(result.segmentDimensionSummaryResult.ToString(), data.ToString());
            Assert.IsType<List<SegmentDimensionSummary>>(result.segmentDimensionSummaryResult);
        }


        [Fact(DisplayName = "Get Design Evaluaion segment Summary By Id No Records Found")]
        public async Task GetSegmentSummaryResult_UnExpectedBehavior()
        {
            var _designService = CreateDesignEvaluationService();

            ExternalServiceResponse<IEnumerable<SegmentDimensionSummary>> responseData = new ExternalServiceResponse<IEnumerable<SegmentDimensionSummary>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SegmentDimensionSummary>(logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var sstTowerId = 1;
            var result = await _designService.GetSegmentDimensionSummaryAsync(sstTowerId);

            Assert.False(result.IsSuccess);
            Assert.Null(result.segmentDimensionSummaryResult);
        }


        [Fact(DisplayName = "Get Design Evaluaion segment Table is Success")]
        public async Task GetSegmentTableAsync_IsSuccess()
        {
            var _designService = CreateDesignEvaluationService();
            List<SegmentDimensionTable> segmentDimensionTable = new List<SegmentDimensionTable>()
            {
                new SegmentDimensionTable()
                {
                    SstUuid= new Guid("14864ebc-886d-49cc-9796-149d3e1c1b10"),
                    SectionNumber= 3,
                    SectionType = "top",
                    SectionLength = 50000,
                    OuterDiameterTop = 3503,
                    OuterDiameterBottom= 4221,
                    MaxPlateThickness = 45,
                    WeightPlates = 82,
                    WeightFlangesTop= 0,
                    WeightFlangesBottom= 2135,
                    TransportWeight= 92
                },

                new SegmentDimensionTable()
                {
                    SstUuid= new Guid("14864ebc-886d-49cc-9796-149d3e1c1b10"),
                    SectionNumber= 2,
                    SectionType= "intermediate",
                    SectionLength= 6360,
                    OuterDiameterTop= 4221,
                    OuterDiameterBottom= 4223,
                    MaxPlateThickness= 24,
                    WeightPlates= 15,
                    WeightFlangesTop= 2135,
                    WeightFlangesBottom= 1801,
                    TransportWeight= 20
                },

                new SegmentDimensionTable()
                {
                    SstUuid= new Guid("14864ebc-886d-49cc-9796-149d3e1c1b10"),
                    SectionNumber= 1,
                    SectionType= "bottom",
                    SectionLength= 31640,
                    OuterDiameterTop= 4223,
                    OuterDiameterBottom= 4480,
                    MaxPlateThickness= 45,
                    WeightPlates= 103,
                    WeightFlangesTop= 1801,
                    WeightFlangesBottom= 0,
                    TransportWeight= 109
                }
            };

            ExternalServiceResponse<IEnumerable<SegmentDimensionTable>> responseData = new ExternalServiceResponse<IEnumerable<SegmentDimensionTable>>()
            {
                ResponseData = segmentDimensionTable,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SegmentDimensionTable>(logger.Object).GetAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var sstUuid = "14864ebc-886d-49cc-9796-149d3e1c1b10";
            var result = await _designService.GetSegmentDimensionTableAsync(sstUuid);

            Assert.True(result.IsSuccess);

            Assert.Equal(result.segmentDimensionTableResult.ToString(), segmentDimensionTable.ToString());
            Assert.IsType<List<SegmentDimensionTable>>(result.segmentDimensionTableResult);
            Assert.NotNull(result.segmentDimensionTableResult);
        }


        [Fact(DisplayName = "Get Design Evaluaion segment Table No Records Found")]
        public async Task GetSegmentTableResult_UnExpectedBehavior()
        {
            var _designService = CreateDesignEvaluationService();
            ExternalServiceResponse<IEnumerable<SegmentDimensionTable>> responseData = new ExternalServiceResponse<IEnumerable<SegmentDimensionTable>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SegmentDimensionTable>(logger.Object).GetAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var sstUuid = "14864ebc-886d-49cc-9796-149d3e1c1b10";
            var result = await _designService.GetSegmentDimensionTableAsync($"$filter=sstUuid eq {sstUuid}");

            Assert.False(result.IsSuccess);
            Assert.Null(result.segmentDimensionTableResult);
        }

        [Fact(DisplayName = "Get Design Evaluaion Summary By Id is Success")]
        public async Task GetDesignEvaluationSummaryIdAsync_IsSuccess()
        {
            var _designService = CreateDesignEvaluationService();
            IEnumerable<Summary> data = new List<Summary>() { new Summary()
               {
                ClusterName = "test",
                DesignLifetime = 10,
                InitialTower = "tdg",
                QuotationId = "test",
                sstTargetLifetime = 10,
                TurbineQuantity = 10,
                WtgType = "ysuu"
                }
            };
            ExternalServiceResponse<IEnumerable<Summary>> responseData = new ExternalServiceResponse<IEnumerable<Summary>>()
            {
                ResponseData = data,
                IsSuccess = true
            };
            mockServiceFactory.Setup(x => x.CreateExternalService<Summary>(logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));
            var id = 1;
            var result = await _designService.GetDESummaryIdAsync(id);
            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Design Evaluaion Summary By Id is Failure")]
        public async Task GetDesignEvaluationSummaryIdAsync_IsFailed()
        {
            var _designService = CreateDesignEvaluationService();
            IEnumerable<Summary> data = new List<Summary>() { };
            ExternalServiceResponse<IEnumerable<Summary>> responseData = new ExternalServiceResponse<IEnumerable<Summary>>()
            {
                ResponseData = data,
                IsSuccess = false
            };
            mockServiceFactory.Setup(x => x.CreateExternalService<Summary>(logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));
            var id = 1;
            var result = await _designService.GetDESummaryIdAsync(id);
            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Design Evaluaion By Id is Success")]
        public async Task GetDesignEvaluationByIdAsync_IsSuccess()
        {
            var _designService = CreateDesignEvaluationService();
            IEnumerable<SstDesignEvaluation> data = new List<SstDesignEvaluation>()
            {
                new SstDesignEvaluation()
                {
                    DesignLifetimeStatus="",
                    Id=1,
                    IsCostsTimelineFeedbackRequest=false
                }
            };
            ExternalServiceResponse<IEnumerable<SstDesignEvaluation>> responseData = new ExternalServiceResponse<IEnumerable<SstDesignEvaluation>>()
            {
                ResponseData = data,
                IsSuccess = true
            };
            mockServiceFactory.Setup(x => x.CreateExternalService<SstDesignEvaluation>(logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));
            var id = 1;
            var result = await _designService.GetDesignEvaluationByIdAsync(id);
            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Design Evaluaion By Id is Failure")]
        public async Task GetDesignEvaluationByIdAsync_IsFailed()
        {
            var _designService = CreateDesignEvaluationService();
            IEnumerable<SstDesignEvaluation> data = new List<SstDesignEvaluation>() { };
            ExternalServiceResponse<IEnumerable<SstDesignEvaluation>> responseData = new ExternalServiceResponse<IEnumerable<SstDesignEvaluation>>()
            {
                ResponseData = data,
                IsSuccess = false
            };
            mockServiceFactory.Setup(x => x.CreateExternalService<SstDesignEvaluation>(logger.Object).GetByIdAsync(It.IsAny<int>())).ReturnsAsync((responseData));
            var id = 1;
            var result = await _designService.GetDesignEvaluationByIdAsync(id);
            Assert.False(result.IsSuccess);
        }
    }
}
