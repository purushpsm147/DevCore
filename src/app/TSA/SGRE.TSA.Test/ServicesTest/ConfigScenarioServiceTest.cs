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
    public class ConfigScenarioServiceTest
    {
        /// <summary>
        /// Defines the IConfigScenarioExternalService
        /// </summary>
        private Mock<IConfigScenarioExternalService> _configScenarioExternalService = new Mock<IConfigScenarioExternalService>();
        private Mock<ILogger<ConfigScenarioService>> Logger = new Mock<ILogger<ConfigScenarioService>>();

        /// <summary>
        /// The ConfigScenarioService
        /// </summary>
        /// <returns></returns>
        private ConfigScenarioService CreateConfigScenarioService()
        {
            return new ConfigScenarioService(_configScenarioExternalService.Object, Logger.Object);
        }

        [Fact(DisplayName = "Get all Scenario")]
        public async Task GetScenarioAsync_IsSuccess()
        {
            var _configScenarioService = CreateConfigScenarioService();

            IEnumerable<Scenario> scenarios = new List<Scenario>() { new Scenario()
            {
                 Id = 1,
                 ScenarioNo = 1,
                 RecordInsertDateTime = DateTime.UtcNow
            } };

            ExternalServiceResponse<IEnumerable<Scenario>> responseData = new ExternalServiceResponse<IEnumerable<Scenario>>()
            {
                ResponseData = scenarios,
                IsSuccess = true
            };

            _configScenarioExternalService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((responseData));

            string configId = "1";
            var result = await _configScenarioService.GetScenarioAsync(configId, 1);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get all the Certifications No Records Found")]
        public async Task GetCertificationAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configScenarioService = CreateConfigScenarioService();

            ExternalServiceResponse<IEnumerable<Scenario>> responseData = new ExternalServiceResponse<IEnumerable<Scenario>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _configScenarioExternalService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(responseData);

            string configId = "1";
            var result = await _configScenarioService.GetScenarioAsync(configId, 1);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get all Cost KPI Scenario")]
        public async Task GetCostKPIScenarioAsync_IsSuccess()
        {
            var _configScenarioService = CreateConfigScenarioService();

            IEnumerable<Scenario> scenarios = new List<Scenario>() { new Scenario()
            {
                 Id = 1,
                 ScenarioNo = 1,

                 RecordInsertDateTime = DateTime.UtcNow
            } };

            ExternalServiceResponse<IEnumerable<Scenario>> responseData = new ExternalServiceResponse<IEnumerable<Scenario>>()
            {
                ResponseData = scenarios,
                IsSuccess = true
            };

            _configScenarioExternalService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((responseData));

            string configId = "1";
            var result = await _configScenarioService.GetScenarioAsync(configId, 1);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Cost KPI Scenario No Records Found")]
        public async Task GetCostKPIScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configScenarioService = CreateConfigScenarioService();

            ExternalServiceResponse<IEnumerable<Scenario>> responseData = new ExternalServiceResponse<IEnumerable<Scenario>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _configScenarioExternalService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(responseData);

            string configId = "1";
            var result = await _configScenarioService.GetScenarioAsync(configId, 1);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Design Scenario")]
        public async Task GetDesignScenarioAsync_IsSuccess()
        {
            var _configScenarioService = CreateConfigScenarioService();

            IEnumerable<Scenario> scenarios = new List<Scenario>() { new Scenario()
            {
                 Id = 1,
                 ScenarioNo = 1,

                 RecordInsertDateTime = DateTime.UtcNow
            } };

            ExternalServiceResponse<IEnumerable<Scenario>> responseData = new ExternalServiceResponse<IEnumerable<Scenario>>()
            {
                ResponseData = scenarios,
                IsSuccess = true
            };

            _configScenarioExternalService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((responseData));

            string configId = "1";
            var result = await _configScenarioService.GetScenarioAsync(configId, 1);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Get Design Scenario Scenario No Records Found")]
        public async Task GetDesignScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configScenarioService = CreateConfigScenarioService();

            ExternalServiceResponse<IEnumerable<Scenario>> responseData = new ExternalServiceResponse<IEnumerable<Scenario>>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _configScenarioExternalService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(responseData);

            string configId = "1";
            var result = await _configScenarioService.GetScenarioAsync(configId, 1);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Get Patch Scenario")]
        public async Task PatchScenarioAsync_IsSuccess()
        {
            var _configScenarioService = CreateConfigScenarioService();

            Scenario scenarios = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,

                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Scenario> responseData = new ExternalServiceResponse<Scenario>()
            {
                ResponseData = scenarios,
                IsSuccess = true
            };

            _configScenarioExternalService.Setup(x => x.PatchScenarioAsync(It.IsAny<int>(), It.IsAny<Scenario>(), true)).ReturnsAsync((responseData));

            int id = 1;
            var result = await _configScenarioService.PatchScenarioAsync(id, scenarios);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Update Scenario No Records Found")]
        public async Task PatchScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configScenarioService = CreateConfigScenarioService();

            Scenario scenarios = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,

                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Scenario> responseData = new ExternalServiceResponse<Scenario>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _configScenarioExternalService.Setup(x => x.PatchScenarioAsync(It.IsAny<int>(), It.IsAny<Scenario>(), true)).ReturnsAsync((responseData));

            int id = 1;
            var result = await _configScenarioService.PatchScenarioAsync(id, scenarios);

            Assert.False(result.IsSuccess);
        }

        [Fact(DisplayName = "Insert New Scenario Record")]
        public async Task PutScenarioAsync_IsSuccess()
        {
            var _configScenarioService = CreateConfigScenarioService();

            Scenario scenarios = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,

                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Scenario> responseData = new ExternalServiceResponse<Scenario>()
            {
                ResponseData = scenarios,
                IsSuccess = true
            };

            _configScenarioExternalService.Setup(x => x.PutScenarioAsync(It.IsAny<Scenario>())).ReturnsAsync((responseData));

            int id = 1;
            var result = await _configScenarioService.PutScenarioAsync(scenarios);

            Assert.True(result.IsSuccess);
        }


        [Fact(DisplayName = "Put Scenario Unsuccessful")]
        public async Task PutScenarioAsync_StateUnderTest_UnExpectedBehavior()
        {
            // Arrange
            var _configScenarioService = CreateConfigScenarioService();

            Scenario scenarios = new Scenario()
            {
                Id = 1,
                ScenarioNo = 1,

                RecordInsertDateTime = DateTime.UtcNow
            };

            ExternalServiceResponse<Scenario> responseData = new ExternalServiceResponse<Scenario>()
            {
                ResponseData = null,
                IsSuccess = false
            };

            _configScenarioExternalService.Setup(x => x.PutScenarioAsync(It.IsAny<Scenario>())).ReturnsAsync((responseData));

            var result = await _configScenarioService.PutScenarioAsync(scenarios);

            Assert.False(result.IsSuccess);
        }
    }
}