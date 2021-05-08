using Moq;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Test.ServicesTest
{
    public class RegionServiceTest
    {

        public static Mock<IRegionExternalService> mock = new Mock<IRegionExternalService>();

        readonly RegionService _regionService;

        public RegionServiceTest()
        {
            _regionService = new RegionService(mock.Object);
        }

        [Fact(DisplayName = "Get a signle Region")]
        public async Task GetRegionAsyncByIdIsSuccess()
        {
            IEnumerable<Region> regionData = new List<Region>()
            { new Region()
            {
                Id = 1,
                RegionName = "APAC"
            }
            };
            ExternalServiceResponse<IEnumerable<Region>> responseData = new ExternalServiceResponse<IEnumerable<Region>>()
            {
                ResponseData = regionData,
                IsSuccess = true
            };

            mock.Setup(x => x.GetRegionsAsync(It.IsAny<int>())).ReturnsAsync(responseData);

            var result = await _regionService.GetRegionAsync(1);
            Assert.NotNull(result.regionResult);

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all Region")]
        public async Task GetRegionAsyncIsSuccess()
        {
            IEnumerable<Region> regionData = new List<Region>()
            {
                new Region () { Id=1, RegionName="APAC" },
                new Region () { Id=2, RegionName="India" },
            };
            ExternalServiceResponse<IEnumerable<Region>> responseData = new ExternalServiceResponse<IEnumerable<Region>>()
            {
                ResponseData = regionData,
                IsSuccess = true
            };

            mock.Setup(x => x.GetRegionsAsync()).ReturnsAsync(responseData);

            var result = await _regionService.GetRegionAsync();

            Assert.NotNull(result.regionResult);
            Assert.True(result.IsSuccess);
        }

    }

}
