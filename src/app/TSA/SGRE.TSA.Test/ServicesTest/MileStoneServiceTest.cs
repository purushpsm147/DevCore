using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ServicesTest
{
    public class MileStoneServiceTest
    {
        /// <summary>
        /// Defines the IMileStonesExternalService
        /// </summary>
        private Mock<IMileStonesExternalService> _milestoneExternalService = new Mock<IMileStonesExternalService>();

        /// <summary>
        /// The MileStoneService
        /// </summary>
        /// <returns></returns>
        private MileStoneService CreateMileStoneService()
        {
            return new MileStoneService(_milestoneExternalService.Object);
        }

        [Fact(DisplayName = "Get all MileStone")]
        public async Task GetMileStoneAsync_IsSuccess()
        {
            // Arrange
            var mileStoneService = CreateMileStoneService();

            IEnumerable<MileStone> mileStones = new List<MileStone>() { new MileStone()
            {
                 Id =1,
                 MileStoneName = "MileStone - 1",
                 RecordInsertDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            },
            new MileStone() {
                 Id =2,
                 MileStoneName = "MileStone - 2",
                 RecordInsertDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<MileStone>> responseData = new ExternalServiceResponse<IEnumerable<MileStone>>()
            {
                ResponseData = mileStones,
                IsSuccess = true
            };

            _milestoneExternalService.Setup(x => x.GetMileStonesAsync()).ReturnsAsync((responseData));

            var result = await mileStoneService.GetMileStoneAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all MileStone No Records Found")]
        public async Task GetMileStoneAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var mileStoneService = CreateMileStoneService();

            IEnumerable<MileStone> mileStones = new List<MileStone>() { new MileStone()
            {
                 Id =1,
                 MileStoneName = "MileStone - 1",
                 RecordInsertDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            },
            new MileStone() {
                 Id =2,
                 MileStoneName = "MileStone - 2",
                 RecordInsertDateTime = DateTime.UtcNow,
                 LastModifiedDateTime = DateTime.UtcNow
            }};

            ExternalServiceResponse<IEnumerable<MileStone>> responseData = new ExternalServiceResponse<IEnumerable<MileStone>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _milestoneExternalService.Setup(x => x.GetMileStonesAsync()).ReturnsAsync((responseData));

            var result = await mileStoneService.GetMileStoneAsync();

            Assert.False(result.IsSuccess);
        }

    }
}