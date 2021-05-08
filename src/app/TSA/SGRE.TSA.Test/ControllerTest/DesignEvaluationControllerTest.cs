using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SGRE.TSA.Api.Controllers;
using SGRE.TSA.Api.Factory;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using Task = System.Threading.Tasks.Task;


namespace SGRE.TSA.Test.ControllerTest
{
    public class DesignEvaluationControllerTest
    {
        private readonly Mock<IDesignEvaluationService> _designEvaluationService = new Mock<IDesignEvaluationService>();
        private readonly Mock<IServiceBusFactory> _serviceBusFactory = new Mock<IServiceBusFactory>();
        private readonly Mock<ISstTowerService> _sstTower = new Mock<ISstTowerService>();
        private readonly Mock<IOpportunityService> _opp = new Mock<IOpportunityService>();
        private readonly Mock<IGMBService> _gmb = new Mock<IGMBService>(); 
        private readonly Mock<IServiceBus> _servicebus = new Mock<IServiceBus>();
        private readonly Mock<IProjectConstraintsService> _pc = new Mock<IProjectConstraintsService>();
        private readonly Mock<ServiceBusSender> _servicebusSender = new Mock<ServiceBusSender>();
        private readonly Mock<ISstDesignRequestServices> _SstDesignRequest = new Mock<ISstDesignRequestServices>();

        private readonly DesignEvaluationController designEvaluationController;

        public DesignEvaluationControllerTest()
        {
            designEvaluationController = new DesignEvaluationController(_designEvaluationService.Object);
        }

        [Fact(DisplayName = "Put RequestDesignEvaluation is Success")]
        public async Task PutRequestDesignEvaluationIsSuccess()
        {
            FrontendRequest frontendRequest = new FrontendRequest()
            {
                sstTowerId = 1,
                action = "Requested",
                scenarioId = 1,
            };

            IEnumerable<RequestDesignEvaluation> requestDesignEvaluations = new List<RequestDesignEvaluation>(){
                new RequestDesignEvaluation()
            {
                DesignAuthor="Anilananda",
                Region="Test"
            }
            };

            _designEvaluationService.Setup(x => x.GetRequestDesignAsync(It.IsAny<int>())).ReturnsAsync((true, requestDesignEvaluations));

            _serviceBusFactory.Setup(x => x.GetServiceBus(It.IsAny<string>())).Returns((_servicebus.Object));
            _servicebusSender.Setup(x => x.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()));
            _servicebus.Setup(x => x.SetServiceBusSender()).Returns(_servicebusSender.Object);


            var result = await designEvaluationController.PutRequestDesignEvaluation(_sstTower.Object, _serviceBusFactory.Object, _pc.Object,  _opp.Object, _gmb.Object, _SstDesignRequest.Object, frontendRequest);

            var okresult = new OkObjectResult(result);
            Assert.Equal(200, okresult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Get Summary of Request Design Evaluation is failure")]
        public async Task GetDesignSummary_IsFailure()
        {
            IEnumerable<Summary> summary = new List<Summary>()
            {


            };
            _designEvaluationService.Setup(x => x.GetDESummaryIdAsync(It.IsAny<int>())).ReturnsAsync((false, summary));
            var result = await designEvaluationController.GetDesignSummary(It.IsAny<int>());

            Assert.Null(result.FirstOrDefault());
        }


        [Fact(DisplayName = "Get segment Summary is success")]
        public async Task GetSegmentSummary_IsSuccess()
        {
            IEnumerable<SegmentDimensionSummary> dimensionSummary = new List<SegmentDimensionSummary>(){
               new SegmentDimensionSummary()
               {
               HubHeight=9999,
                       NumberOfSections=3,
                       SstTowerId=1,
                       TowerHeight=999
                }
            };

            _designEvaluationService.Setup(x => x.GetSegmentDimensionSummaryAsync(It.IsAny<int>())).ReturnsAsync((true, dimensionSummary));
            var result = await designEvaluationController.GetSegmentDimensionSummary(_designEvaluationService.Object, 1);
            var okresult = new OkObjectResult(result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Get segment Summary is failure")]
        public async Task GetSegmentSummary_IsFailure()
        {
            IEnumerable<SegmentDimensionSummary> summary = new List<SegmentDimensionSummary>()
            {
            };
            _designEvaluationService.Setup(x => x.GetSegmentDimensionSummaryAsync(It.IsAny<int>())).ReturnsAsync((false, summary));
            var result = await designEvaluationController.GetSegmentDimensionSummary(_designEvaluationService.Object, It.IsAny<int>());

            Assert.Null(result.FirstOrDefault());
        }


        [Fact(DisplayName = "Get segment Table is success")]
        public async Task GetSegmentTable_IsSuccess()
        {
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


            _designEvaluationService.Setup(x => x.GetSegmentDimensionTableAsync(It.IsAny<string>())).ReturnsAsync((true, segmentDimensionTable));
            var result = await designEvaluationController.GetSegmentDimensionTable(_designEvaluationService.Object, "14864ebc-886d-49cc-9796-149d3e1c1b10");

            Assert.NotNull(result);

        }

        [Fact(DisplayName = "Put Design Evaluation")]
        public async Task PutDesignEvaluationIsSuccess()
        {
            SstDesignEvaluation sstDesignEvaluation = new SstDesignEvaluation()
            {
                sstTowerId = 96,
                DesignLifetimeStatus = "ELSA/SAFAL dataset and SST input target lifetime is not consistent"
            };

            _designEvaluationService.Setup(x => x.PutDesignEvaluationAsync(It.IsAny<SstDesignEvaluation>())).ReturnsAsync((true, sstDesignEvaluation));

            var result = await designEvaluationController.PutDesignEvaluation(_designEvaluationService.Object, sstDesignEvaluation);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Design Evaluation Record")]
        public async Task PutProposalFailure()
        {
            SstDesignEvaluation sstDesignEvaluation = new SstDesignEvaluation()
            {
                sstTowerId = 0,
                DesignLifetimeStatus = "ELSA/SAFAL dataset and SST input target lifetime is not consistent"
            };

            _designEvaluationService.Setup(x => x.PutDesignEvaluationAsync(It.IsAny<SstDesignEvaluation>())).ReturnsAsync((false, sstDesignEvaluation));

            var result = await designEvaluationController.PutDesignEvaluation(_designEvaluationService.Object, sstDesignEvaluation);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }


        [Fact(DisplayName = "Patch Design Evaluation passed")]
        public async Task PatchDesignEvaluationIsSuccess()
        {
            SstDesignEvaluation sstDesignEvaluation = new SstDesignEvaluation()
            {
                sstTowerId = 96,
                DesignLifetimeStatus = "ELSA/SAFAL dataset and SST input target lifetime is not consistent"
            };

            _designEvaluationService.Setup(x => x.PatchDesignEvaluationAsync(It.IsAny<SstDesignEvaluation>())).ReturnsAsync((true, sstDesignEvaluation));
            var result = await designEvaluationController.PatchDesignEvaluation(_designEvaluationService.Object, sstDesignEvaluation);
            var okresult = new OkObjectResult(result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Update Design Evaluation Record")]
        public async Task PatchProposalIsFailure()
        {
            SstDesignEvaluation sstDesignEvaluation = new SstDesignEvaluation()
            {
                sstTowerId = 0,
                DesignLifetimeStatus = "ELSA/SAFAL dataset and SST input target lifetime is not consistent"
            };

            _designEvaluationService.Setup(x => x.PatchDesignEvaluationAsync(It.IsAny<SstDesignEvaluation>())).ReturnsAsync((false, sstDesignEvaluation));


            var result = await designEvaluationController.PatchDesignEvaluation(_designEvaluationService.Object, sstDesignEvaluation);
        }

        [Fact(DisplayName = "Get segment Table is failure")]
        public async Task GetSegmentTable_IsFailure()
        {
            IEnumerable<SegmentDimensionTable> segmentDimensionTable = new List<SegmentDimensionTable>()
            {
            };
            _designEvaluationService.Setup(x => x.GetSegmentDimensionTableAsync(It.IsAny<string>())).ReturnsAsync((false, segmentDimensionTable));
            var result = await designEvaluationController.GetSegmentDimensionTable(_designEvaluationService.Object, It.IsAny<string>());

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
            Assert.Null(result.FirstOrDefault());
        }

    }
}
