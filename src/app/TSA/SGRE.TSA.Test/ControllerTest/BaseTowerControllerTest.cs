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
    public class BaseTowerControllerTest
    {
        readonly BaseTowerController _baseTowerController;

        private readonly Mock<IBaseTowerService> _baseTowerService = new Mock<IBaseTowerService>();

        public BaseTowerControllerTest()
        {
            _baseTowerController = new BaseTowerController(_baseTowerService.Object);
        }


        [Fact(DisplayName = "Get Base Tower Details By Tower ID")]
        public async Task GetBaseTowerIsSuccess()
        {
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

            _baseTowerService.Setup(x => x.GetBaseTowerAsync(It.IsAny<int>())).ReturnsAsync((true, baseTowers));

            var baseTowerId = 1;
            var result = await _baseTowerController.GetBaseTower(baseTowerId);

            Assert.Equal(result, baseTowers);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Data for Get Base Tower Details By Base Tower ID")]
        public async Task GetSSTInputIsFailure()
        {
            IEnumerable<BaseTower> baseTowers = new List<BaseTower>() { };

            _baseTowerService.Setup(x => x.GetBaseTowerAsync(It.IsAny<int>())).ReturnsAsync((true, baseTowers));

            var baseTowerId = 1;
            var result = await _baseTowerController.GetBaseTower(baseTowerId);

            Assert.Equal(result, baseTowers);
            Assert.Empty(result);
        }

        [Fact(DisplayName = "No Data for Get Base Tower Details By Base Tower ID")]
        public async Task GetSSTInputIsunExpectedBehaviour()
        {
            IEnumerable<BaseTower> baseTowers = new List<BaseTower>() { };

            _baseTowerService.Setup(x => x.GetBaseTowerAsync(It.IsAny<int>())).ReturnsAsync((false, baseTowers));

            var baseTowerId = 1;
            var result = await _baseTowerController.GetBaseTower(baseTowerId);

            Assert.Equal(result, baseTowers);
            Assert.Empty(result);
        }


         [Fact(DisplayName = "Insert New Base Tower Record")]
         public async Task PutBaseTowerIsSuccess()
         {
            BaseTower baseTower =  new BaseTower()
            {
                 TowerTypeId = 1,
                 ClusterSize = 10,
                 HubHeight = 101,
                 IsSarInputRequest = false,
                 ApplicationModes = new int[] { 1, 5, 7 },
                 LoadsClusterId = 1,
                 RecordInsertDateTime = DateTime.UtcNow
            };

            BaseTower ResponseBaseTower = new BaseTower()
            {
                 Id = 1,
                 TowerTypeId = 1,
                 ClusterSize = 10,
                 HubHeight = 101,
                 IsSarInputRequest = false,
                 ApplicationModes = new int[]{ 1,5,7 },
                 LoadsClusterId = 1,
                 RecordInsertDateTime = DateTime.UtcNow
            };

            _baseTowerService.Setup(x => x.PutBaseTowerAsync(It.IsAny<BaseTower>())).ReturnsAsync((true, ResponseBaseTower));

            var result = await _baseTowerController.PutBaseTower(baseTower);

            Assert.IsType<OkObjectResult>(result);
        }

         [Fact(DisplayName = "Failed to Insert New SST Input Record")]
         public async Task PutSstIsFailure()
         {
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

            _baseTowerService.Setup(x => x.PutBaseTowerAsync(It.IsAny<BaseTower>())).ReturnsAsync((false, baseTower));

            var result = await _baseTowerController.PutBaseTower(baseTower);

            Assert.IsType<BadRequestObjectResult>(result);
         }
    }

}