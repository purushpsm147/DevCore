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
    public class SSTInputTest
    {

        private Mock<IExternalServiceFactory> mockServiceFactory = new Mock<IExternalServiceFactory>();

        private Mock<ILogger<SstTowerService>> _mockSstTowerServiceLogger = new Mock<ILogger<SstTowerService>>();

        private Mock<ILogger<WtgCatalogue>> _mockWtgCatalogueLogger = new Mock<ILogger<WtgCatalogue>>();
        private Mock<IConfigScenarioService> _configScenarioService = new Mock<IConfigScenarioService>();

        /// <summary>
        /// Defines the IConfigScenarioExternalService
        /// </summary>
        private Mock<IConfigScenarioExternalService> _configScenarioExternalService = new Mock<IConfigScenarioExternalService>();

        /// <summary>
        /// The SSTInputService
        /// </summary>
        /// <returns></returns>
        private SstTowerService CreateSSTInputService()
        {
            return new SstTowerService(mockServiceFactory.Object, _configScenarioExternalService.Object, _mockSstTowerServiceLogger.Object, _configScenarioService.Object);
        }

        [Fact(DisplayName = "Get all SST Id")]
        public async Task GetSSTIdAsync_IsSuccess()
        {
            var _configSSTInputService = CreateSSTInputService();

            IEnumerable<SstTower> sstInputs = new List<SstTower>() { new SstTower()
            {
                 Id = 1,
                 TowerTypeId = 1,
                 ClusterSize = 10,
                 RecordInsertDateTime = DateTime.UtcNow
            } };

            ExternalServiceResponse<IEnumerable<SstTower>> responseData = new ExternalServiceResponse<IEnumerable<SstTower>>()
            {
                ResponseData = sstInputs,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstTower>(_mockSstTowerServiceLogger.Object).GetAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var sstInput = 1;
            var result = await _configSSTInputService.GetSstTowerIdAsync(sstInput);

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all the SST Input No Records Found")]
        public async Task GetSSTIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configSSTInputService = CreateSSTInputService();

            ExternalServiceResponse<IEnumerable<SstTower>> responseData = new ExternalServiceResponse<IEnumerable<SstTower>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstTower>(_mockSstTowerServiceLogger.Object).GetAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var sstInput = 1;
            var result = await _configSSTInputService.GetSstTowerIdAsync(sstInput);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Patch SSTinput")]
        public async Task PatchSSTAsync_IsSuccess()
        {
            var _configSSTInputService = CreateSSTInputService();

            SstTower sstInputs = new SstTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,
                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<SstTower> responseData = new ExternalServiceResponse<SstTower>()
            {
                ResponseData = sstInputs,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstTower>(_mockSstTowerServiceLogger.Object).PatchAsync(It.IsAny<int>(), It.IsAny<SstTower>())).ReturnsAsync((responseData));

            int id = 1;
            var result = await _configSSTInputService.PatchSstTowerAsync(id, sstInputs);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Update SSTInput No Records Found")]
        public async Task PatchSSTInputAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configSSTInputService = CreateSSTInputService();

            SstTower sstInputs = new SstTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,
                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<SstTower> responseData = new ExternalServiceResponse<SstTower>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstTower>(_mockSstTowerServiceLogger.Object).PatchAsync(It.IsAny<int>(), It.IsAny<SstTower>())).ReturnsAsync((responseData));

            int id = 1;
            var result = await _configSSTInputService.PatchSstTowerAsync(id, sstInputs);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert New SSTInput Record")]
        public async Task PutSSTInputAsync_IsSuccess()
        {
            var _configSSTInputService = CreateSSTInputService();

            SstTower sstInputs = new SstTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,
                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<SstTower> responseData = new ExternalServiceResponse<SstTower>()
            {
                ResponseData = sstInputs,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstTower>(_mockSstTowerServiceLogger.Object).PutAsync(It.IsAny<SstTower>())).ReturnsAsync((responseData));

            Scenario scenarios = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,

                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Scenario> scenarioResponseData = new ExternalServiceResponse<Scenario>()
            {
                ResponseData = scenarios,
                IsSuccess = true
            };

            _configScenarioExternalService.Setup(x => x.PutScenarioAsync(It.IsAny<Scenario>())).ReturnsAsync((scenarioResponseData));


            var result = await _configSSTInputService.PutSstTowerAsync(sstInputs);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Put Scenario Unsuccessful")]
        public async Task PutScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configSSTInputService = CreateSSTInputService();

            SstTower sstInputs = new SstTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,
                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<SstTower> responseData = new ExternalServiceResponse<SstTower>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<SstTower>(_mockSstTowerServiceLogger.Object).PutAsync(It.IsAny<SstTower>())).ReturnsAsync((responseData));

            var result = await _configSSTInputService.PutSstTowerAsync(sstInputs);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Initial Tower")]
        public async Task GetInitialTowerAsync_IsSuccess()
        {
            var _configSSTInputService = CreateSSTInputService();

            List<InitialTower> initialTowers = new List<InitialTower>() { new InitialTower()
            {
                 Id = 1,
                 HubHeightMinM = 90,
                 HubHeightMaxM = 100,
                 Model = "",
                 Tower = "T90.41"
            } };

            List<WtgCatalogue> WtgCatalogue = new List<WtgCatalogue>(){  new WtgCatalogue()
            {
                 Id = 1,
                 InitialTowers = initialTowers
            } };

            ExternalServiceResponse<IEnumerable<WtgCatalogue>> responseData = new ExternalServiceResponse<IEnumerable<WtgCatalogue>>()
            {
                ResponseData = WtgCatalogue,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<WtgCatalogue>(_mockSstTowerServiceLogger.Object).GetAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var WtgCatalogueid = 1;
            var ProposedHubHeight = 120;
            var WtgCatalogueModelId = 1;

            var result = await _configSSTInputService.GetIntialTowerAsync(WtgCatalogueid, WtgCatalogueModelId, ProposedHubHeight);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.sstResults);
        }

        [Fact(DisplayName = "Get all the InitialTower No Records Found")]
        public async Task GetInitialTowerAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configSSTInputService = CreateSSTInputService();

            ExternalServiceResponse<IEnumerable<WtgCatalogue>> responseData = new ExternalServiceResponse<IEnumerable<WtgCatalogue>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<WtgCatalogue>(_mockSstTowerServiceLogger.Object).GetAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var WtgCatalogueid = 1;
            var ProposedHubHeight = 120;
            var WtgCatalogueModelId = 1;
            var result = await _configSSTInputService.GetIntialTowerAsync(WtgCatalogueid, WtgCatalogueModelId, ProposedHubHeight);

            Assert.False(result.IsSuccess);
            Assert.Null(result.sstResults);
        }
    }
}