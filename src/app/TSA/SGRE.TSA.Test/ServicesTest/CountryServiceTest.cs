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
    public class CountryServiceTest
    {
        /// <summary>
        /// Defines the ICountryExternalService
        /// </summary>
        private Mock<ICountryExternalService> _countryExternalService = new Mock<ICountryExternalService>();

        /// <summary>
        /// The CountryService
        /// </summary>
        /// <returns></returns>
        private CountryService CreateCountryService()
        {
            return new CountryService(_countryExternalService.Object);
        }

        [Fact(DisplayName = "Get all Country")]
        public async Task GetCountryAsync_IsSuccess()
        {
            var countryService = CreateCountryService();
            

            IEnumerable<Country> data = new List<Country>() { new Country()
            {
                Id = 1,
                CountryName = "Certification - 1"
            },
            new Country()
            {
                Id = 2,
                CountryName = "Certification - 2"
            }};

            ExternalServiceResponse<IEnumerable<Country>> responseData = new ExternalServiceResponse<IEnumerable<Country>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _countryExternalService.Setup(x => x.GetCountryAsync()).ReturnsAsync((responseData));

            var result = await countryService.GetCountryAsync();

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Country No Records Found")]
        public async Task GetCountryAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var countryService = CreateCountryService();

            IEnumerable<Country> data = new List<Country>() { new Country()
            {
                Id = 1,
                CountryName = "Certification - 1"
            },
            new Country()
            {
                Id = 2,
                CountryName = "Certification - 2"
            }};

            ExternalServiceResponse<IEnumerable<Country>> responseData = new ExternalServiceResponse<IEnumerable<Country>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _countryExternalService.Setup(x => x.GetCountryAsync()).ReturnsAsync((responseData));

            var result = await countryService.GetCountryAsync();

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all Country By Id")]
        public async Task GetCountryByIdAsync_IsSuccess()
        {
            var countryService = CreateCountryService();

            IEnumerable<Country> data = new List<Country>() { new Country()
            {
                Id = 1,
                CountryName = "Certification - 1"
            },
            new Country()
            {
                Id = 2,
                CountryName = "Certification - 2"
            }};

            ExternalServiceResponse<IEnumerable<Country>> responseData = new ExternalServiceResponse<IEnumerable<Country>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _countryExternalService.Setup(x => x.GetCountryAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await countryService.GetCountryAsync(id);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Country by Id No Records Found")]
        public async Task GetCountryByIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var countryService = CreateCountryService();

            IEnumerable<Country> data = new List<Country>() { new Country()
            {
                Id = 1,
                CountryName = "Certification - 1"
            },
            new Country()
            {
                Id = 2,
                CountryName = "Certification - 2"
            }};

            ExternalServiceResponse<IEnumerable<Country>> responseData = new ExternalServiceResponse<IEnumerable<Country>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _countryExternalService.Setup(x => x.GetCountryAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var id = 1;
            var result = await countryService.GetCountryAsync(id);

            Assert.False(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Country By RegionId")]
        public async Task GetCountryByRegionIdAsync_IsSuccess()
        {
            var countryService = CreateCountryService();

            IEnumerable<Country> data = new List<Country>() { new Country()
            {
                Id = 1,
                CountryName = "Certification - 1"
            },
            new Country()
            {
                Id = 2,
                CountryName = "Certification - 2"
            }};

            ExternalServiceResponse<IEnumerable<Country>> responseData = new ExternalServiceResponse<IEnumerable<Country>>()
            {
                ResponseData = data,
                IsSuccess = true
            };

            _countryExternalService.Setup(x => x.GetCountryByRegionAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var RegionId = 1;
            var result = await countryService.GetCountryByRegionAsync(RegionId);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all Country by RegionId No Records Found")]
        public async Task GetCountryByRegionIdAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var countryService = CreateCountryService();

            IEnumerable<Country> data = new List<Country>() { new Country()
            {
                Id = 1,
                CountryName = "Certification - 1"
            },
            new Country()
            {
                Id = 2,
                CountryName = "Certification - 2"
            }};

            ExternalServiceResponse<IEnumerable<Country>> responseData = new ExternalServiceResponse<IEnumerable<Country>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _countryExternalService.Setup(x => x.GetCountryByRegionAsync(It.IsAny<int>())).ReturnsAsync((responseData));

            var RegionId = 1;
            var result = await countryService.GetCountryByRegionAsync(RegionId);

            Assert.False(result.IsSuccess);
        }
    }
}