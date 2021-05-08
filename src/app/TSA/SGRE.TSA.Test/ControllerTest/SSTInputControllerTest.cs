using Microsoft.AspNetCore.Mvc;
using Moq;
using SGRE.TSA.Api.Controllers;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ControllerTest
{
    public class SSTInputControllerTest
    {
        readonly SstTowerController _sstTowerController;

        private readonly Mock<ISstTowerService> _sstTowerService = new Mock<ISstTowerService>();

        public SSTInputControllerTest()
        {
            _sstTowerController = new SstTowerController(_sstTowerService.Object);
        }

        [Fact(DisplayName = "Get SSTInput Details By SSTInput ID")]
        public async Task GetSSTInputIsSuccess()
        {
            IEnumerable<SstTower> sstInputs = new List<SstTower>() { new SstTower()
            {
                 Id = 1,
                 TowerTypeId = 1,
                 ClusterSize = 10,

                 RecordInsertDateTime = DateTime.UtcNow
            } };

            _sstTowerService.Setup(x => x.GetSstTowerIdAsync(It.IsAny<int>())).ReturnsAsync((true, sstInputs));

            var sstInputId = 1;
            var result = await _sstTowerController.GetSstTowerById(sstInputId);

            Assert.Equal(result, sstInputs);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Data for Get SSTInput Details By SSTInput ID")]
        public async Task GetSSTInputIsFailure()
        {
            IEnumerable<SstTower> sstInputs = new List<SstTower>() { };

            _sstTowerService.Setup(x => x.GetSstTowerIdAsync(It.IsAny<int>())).ReturnsAsync((true, sstInputs));

            var sstInputId = 1;
            var result = await _sstTowerController.GetSstTowerById(sstInputId);

            Assert.Equal(result, sstInputs);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "Insert New SST Input Record")]
        public async Task PutSSTInputIsSuccess()
        {
            SstTower sstInput = new SstTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,

                RecordInsertDateTime = DateTime.UtcNow
            };

            _sstTowerService.Setup(x => x.PutSstTowerAsync(It.IsAny<SstTower>())).ReturnsAsync((true, sstInput));

            var result = await _sstTowerController.PutSstTower(sstInput);

            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New SST Input Record")]
        public async Task PutSstIsFailure()
        {
            SstTower sstInput = new SstTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,

                RecordInsertDateTime = DateTime.UtcNow
            };

            _sstTowerService.Setup(x => x.PutSstTowerAsync(It.IsAny<SstTower>())).ReturnsAsync((false, sstInput));

            var result = await _sstTowerController.PutSstTower(sstInput);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "Update Scenario Record")]
        public async Task PatchMyScenarioIsSuccess()
        {
            SstTower sstInput = new SstTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,

                RecordInsertDateTime = DateTime.UtcNow
            };

            _sstTowerService.Setup(x => x.PatchSstTowerAsync(It.IsAny<int>(), It.IsAny<SstTower>())).ReturnsAsync((true, sstInput));

            var id = 1;
            var result = await _sstTowerController.PatchSstTower(id, sstInput);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Scenario Record")]
        public async Task PatchMyScenarioIsFailure()
        {
            SstTower sstInput = new SstTower()
            {
                Id = 1,
                TowerTypeId = 1,
                ClusterSize = 10,

                RecordInsertDateTime = DateTime.UtcNow
            };

            _sstTowerService.Setup(x => x.PatchSstTowerAsync(It.IsAny<int>(), It.IsAny<SstTower>())).ReturnsAsync((true, sstInput));

            var id = 1;
            var result = await _sstTowerController.PatchSstTower(id, sstInput);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "Get Initial Tower")]
        public async Task GetInitialTowerIsSuccess()
        {
            List<InitialTower> initialTowers = new List<InitialTower>() { new InitialTower()
            {
                 Id = 1,
                 HubHeightMinM = 90,
                 HubHeightMaxM = 100,
                 Model = "",
                 Tower = "T90.41"
            } };

            _sstTowerService.Setup(x => x.GetIntialTowerAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>())).ReturnsAsync((true, initialTowers));

            var WtgCatalogueid = 1;
            var ProposedHubHeight = 120;
            var WtgCatalogueModelId = 1;
            var result = await _sstTowerController.GetInitialTower(WtgCatalogueid, WtgCatalogueModelId, ProposedHubHeight);

            Assert.Equal(result, initialTowers);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Data for Get Initial Tower")]
        public async Task GetInitialTowerIsFailure()
        {
            IEnumerable<InitialTower> initialTowers = new List<InitialTower>() { };

            _sstTowerService.Setup(x => x.GetIntialTowerAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>())).ReturnsAsync((true, initialTowers));

            var WtgCatalogueid = 1;
            var ProposedHubHeight = 120;
            var WtgCatalogueModelId = 1;
            var result = await _sstTowerController.GetInitialTower(WtgCatalogueid, WtgCatalogueModelId, ProposedHubHeight);

            Assert.Equal(result, initialTowers);
            Assert.Empty(result);
        }
    }

}