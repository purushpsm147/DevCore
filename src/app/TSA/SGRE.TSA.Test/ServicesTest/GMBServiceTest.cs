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
    public class GMBServiceTest
    {
        /// <summary>
        /// Defines the IGMBExternalService
        /// </summary>
        private Mock<IGMBExternalService> _gMBExternalService = new Mock<IGMBExternalService>();

        /// <summary>
        /// The GMBService
        /// </summary>
        /// <returns></returns>
        private GMBService CreateCountryService()
        {
            return new GMBService(_gMBExternalService.Object);
        }

        [Fact(DisplayName = "Get all Generic Market Boundary")]
        public async Task GetGMBAsync_IsSuccess()
        {
            var createCountryService = CreateCountryService();

            IEnumerable<GenericMarketBoundary> data = new List<GenericMarketBoundary>() { new GenericMarketBoundary()
            {
                Id = 1,
                CountryId =1,
                MaxTowerBaseDiameterMtrs = 10,
                LastModifiedDateTime = DateTime.Now
            },
            new GenericMarketBoundary()
            {
                Id = 2,
                CountryId = 1,
                MaxTowerBaseDiameterMtrs = 10,
                LastModifiedDateTime = DateTime.Now
            }};

            ExternalServiceResponse<IEnumerable<GenericMarketBoundary>> responseData = new ExternalServiceResponse<IEnumerable<GenericMarketBoundary>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _gMBExternalService.Setup(x => x.GetGMBAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var countryId = 1;
            var result = await createCountryService.GetGMBAsync(countryId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Generic Market Boundary No Records Found")]
        public async Task GetGMBAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var createCountryService = CreateCountryService();

            IEnumerable<GenericMarketBoundary> data = new List<GenericMarketBoundary>() { new GenericMarketBoundary()
            {
                Id = 1,
                CountryId =1,
                MaxTowerBaseDiameterMtrs = 10,
                LastModifiedDateTime = DateTime.Now
            },
            new GenericMarketBoundary()
            {
                Id = 2,
                CountryId = 1,
                MaxTowerBaseDiameterMtrs = 10,
                LastModifiedDateTime = DateTime.Now
            }};

            ExternalServiceResponse<IEnumerable<GenericMarketBoundary>> responseData = new ExternalServiceResponse<IEnumerable<GenericMarketBoundary>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _gMBExternalService.Setup(x => x.GetGMBAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var countryId = 1;
            var result = await createCountryService.GetGMBAsync(countryId);

            Assert.False(result.IsSuccess);
        }

    }
}