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
    public class BaseTowerServiceTest
    {

        private Mock<IExternalServiceFactory> mockServiceFactory = new Mock<IExternalServiceFactory>();

        private Mock<ILogger<BaseTowerService>> _mockBaseTowerServiceLogger = new Mock<ILogger<BaseTowerService>>();

        /// <summary>
        /// Defines the IConfigScenarioExternalService
        /// </summary>
        private Mock<IConfigScenarioExternalService> _configScenarioExternalService = new Mock<IConfigScenarioExternalService>();
        private Mock<IConfigScenarioService> _configScenarioService = new Mock<IConfigScenarioService>();

        /// <summary>
        /// The BaseTowerService
        /// </summary>
        /// <returns></returns>
        private BaseTowerService CreateBaseTowerService()
        {
            return new BaseTowerService(mockServiceFactory.Object, _configScenarioExternalService.Object, _mockBaseTowerServiceLogger.Object, _configScenarioService.Object);
        }

        [Fact(DisplayName = "Get all Base Tower Id")]
        public async Task GetBaseTowerByIdAsync_IsSuccess()
        {
            var _baseTowerService = CreateBaseTowerService();


            IEnumerable<BaseTower> baseTowers = new List<BaseTower>() { new BaseTower()
            {
                 Id = 1,
                 TowerTypeId = 1,
                 ClusterSize = 10,
                 HubHeight = 101,
                 IsSarInputRequest = false,
                 ApplicationModes = new int[]{ 1,5,7 },
                 LoadsClusterId = 1,
                 RecordInsertDateTime = DateTime.UtcNow
            } };


            ExternalServiceResponse<IEnumerable<BaseTower>> responseData = new ExternalServiceResponse<IEnumerable<BaseTower>>()
            {
                ResponseData = baseTowers,
                IsSuccess = true
            };


            mockServiceFactory.Setup(x => x.CreateExternalService<BaseTower>(_mockBaseTowerServiceLogger.Object).GetAsync(It.IsAny<string>())).ReturnsAsync((responseData));


            var baseTowerId = 1;
            var result = await _baseTowerService.GetBaseTowerAsync(baseTowerId);

            Assert.True(result.IsSuccess);
            Assert.Equal(result.baseTowerResults.ToString(), baseTowers.ToString());
            Assert.IsType<List<BaseTower>>(result.baseTowerResults);
        }

        [Fact(DisplayName = "Get all the Base Tower Id No Records Found")]
        public async Task GetBaseTowerById_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var baseTowerService = CreateBaseTowerService();

            ExternalServiceResponse<IEnumerable<BaseTower>> responseData = new ExternalServiceResponse<IEnumerable<BaseTower>>()
            {
                ResponseData = null,
                IsSuccess = false
            };


            mockServiceFactory.Setup(x => x.CreateExternalService<BaseTower>(_mockBaseTowerServiceLogger.Object).GetAsync(It.IsAny<string>())).ReturnsAsync((responseData));

            var baseTowerId = 1;
            var result = await baseTowerService.GetBaseTowerAsync(baseTowerId);

            Assert.False(result.IsSuccess);
            Assert.Null(result.baseTowerResults);
        }

        [Fact(DisplayName = "Insert New Base Tower Record")]
        public async Task PutBaseTowerAsync_IsSuccess()
        {
            // Arrange
            var baseTowerService = CreateBaseTowerService();

            BaseTower baseTower = new BaseTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,
                HubHeight = 101,
                IsSarInputRequest = false,
                ApplicationModes = new int[] { 1, 5, 7 },
                LoadsClusterId = 1,
                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<BaseTower> responseData = new ExternalServiceResponse<BaseTower>()
            {
                ResponseData = baseTower,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<BaseTower>(_mockBaseTowerServiceLogger.Object).PutAsync(It.IsAny<BaseTower>())).ReturnsAsync((responseData));

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

            var result = await baseTowerService.PutBaseTowerAsync(baseTower);

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert Base Tower Unsuccessful")]
        public async Task PutScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var baseTowerService = CreateBaseTowerService();

            BaseTower baseTower = new BaseTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,
                HubHeight = 101,
                IsSarInputRequest = false,
                ApplicationModes = new int[] { 1, 5, 7 },
                LoadsClusterId = 1,
                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<BaseTower> responseData = new ExternalServiceResponse<BaseTower>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<BaseTower>(_mockBaseTowerServiceLogger.Object).PutAsync(It.IsAny<BaseTower>())).ReturnsAsync((responseData));

            var result = await baseTowerService.PutBaseTowerAsync(baseTower);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Update Existing Base Tower Record")]
        public async Task PatchBaseTowerAsync_IsSuccess()
        {
            // Arrange
            var baseTowerService = CreateBaseTowerService();

            BaseTower baseTower = new BaseTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,
                HubHeight = 101,
                IsSarInputRequest = false,
                ApplicationModes = new int[] { 1, 5, 7 },
                LoadsClusterId = 1,
                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<BaseTower> responseData = new ExternalServiceResponse<BaseTower>()
            {
                ResponseData = baseTower,
                IsSuccess = true
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<BaseTower>(_mockBaseTowerServiceLogger.Object).PatchAsync(It.IsAny<int>(), It.IsAny<BaseTower>())).ReturnsAsync((responseData));

            int patchId = 1;
            var result = await baseTowerService.PatchBaseTowerAsync(patchId, baseTower);

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Update Base Tower Unsuccessful")]
        public async Task PatchScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var baseTowerService = CreateBaseTowerService();

            BaseTower baseTower = new BaseTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,
                HubHeight = 101,
                IsSarInputRequest = false,
                ApplicationModes = new int[] { 1, 5, 7 },
                LoadsClusterId = 1,
                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<BaseTower> responseData = new ExternalServiceResponse<BaseTower>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            mockServiceFactory.Setup(x => x.CreateExternalService<BaseTower>(_mockBaseTowerServiceLogger.Object).PatchAsync(It.IsAny<int>(), It.IsAny<BaseTower>())).ReturnsAsync((responseData));


            int patchId = 1;
            var result = await baseTowerService.PatchBaseTowerAsync(patchId, baseTower);

            Assert.False(result.IsSuccess);
        }
    }
}