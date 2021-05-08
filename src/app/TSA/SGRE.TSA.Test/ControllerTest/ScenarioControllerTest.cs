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
    public class ScenarioControllerTest
    {
        readonly ScenarioController _scenarioController;

        private readonly Mock<IConfigScenarioService> _configScenarioService = new Mock<IConfigScenarioService>();
        private readonly Mock<ISstTowerService> _SstTowerService = new Mock<ISstTowerService>();
        private readonly Mock<IBaseTowerService> _baseTowerService = new Mock<IBaseTowerService>();

        public ScenarioControllerTest()
        {
            _scenarioController = new ScenarioController(_configScenarioService.Object);
        }


        [Fact(DisplayName = "Get All Design Scenarios")]
        public async Task GetDesignScenarioIsSuccess()
        {
            IEnumerable<Scenario> scenarios = new List<Scenario>() { new Scenario()
            {
                 Id = 1,
                 ScenarioNo = 1,

                 RecordInsertDateTime = DateTime.UtcNow
            } };

            _configScenarioService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((true, scenarios));

            var configId = "1";
            var result = await _scenarioController.GetScenario(configId, 1);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Data Get All Design Scenarios")]
        public async Task GetDesignScenarioIsFailure()
        {
            IEnumerable<Scenario> scenarios = new List<Scenario>() { };

            _configScenarioService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((true, scenarios));

            var configId = "1";
            var result = await _scenarioController.GetScenario(configId, 1);

            Assert.Equal(result, scenarios);
        }


        [Fact(DisplayName = "Get All Cost KPI Scenario")]
        public async Task GetCostKPIScenarioIsSuccess()
        {
            IEnumerable<Scenario> scenarios = new List<Scenario>() { new Scenario()
            {
                 Id = 1,
                 ScenarioNo = 1,

                 RecordInsertDateTime = DateTime.UtcNow
            } };

            _configScenarioService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((true, scenarios));

            var configId = "1";
            var result = await _scenarioController.GetScenario(configId, 1);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "No Data Get All Cost KPI Scenario")]
        public async Task GetCostKPIScenarioIsFailure()
        {
            IEnumerable<Scenario> scenarios = new List<Scenario>() { };

            _configScenarioService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((true, scenarios));

            var configId = "1";
            var result = await _scenarioController.GetScenario(configId, 1);

            Assert.Equal(result, scenarios);
        }

        [Fact(DisplayName = "Insert New Scenario Record")]
        public async Task PutMyScenarioIsSuccess()
        {
            Scenario scenario = new Scenario()
            {

                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow
            };

            _configScenarioService.Setup(x => x.PutScenarioAsync(It.IsAny<Scenario>())).ReturnsAsync((true, scenario));

            var result = await _scenarioController.PutMyScenario(scenario, _SstTowerService.Object, _baseTowerService.Object);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Scenario Record")]
        public async Task PutMyScenarioIsFailure()
        {
            Scenario scenario = new Scenario()
            {

                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow
            };

            _configScenarioService.Setup(x => x.PutScenarioAsync(It.IsAny<Scenario>())).ReturnsAsync((false, scenario));

            var result = await _scenarioController.PutMyScenario(scenario, _SstTowerService.Object, _baseTowerService.Object);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "Update Scenario Record")]
        public async Task PatchMyScenarioIsSuccess()
        {
            Scenario scenario = new Scenario()
            {

                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow
            };

            _configScenarioService.Setup(x => x.PatchScenarioAsync(It.IsAny<int>(), It.IsAny<Scenario>(), true)).ReturnsAsync((true, scenario));

            var id = 1;
            var result = await _scenarioController.PatchMyScenario(id, scenario);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Failed to Insert New Scenario Record")]
        public async Task PatchMyScenarioIsFailure()
        {
            Scenario scenario = new Scenario()
            {

                ScenarioNo = 1,
                RecordInsertDateTime = DateTime.UtcNow
            };

            _configScenarioService.Setup(x => x.PatchScenarioAsync(It.IsAny<int>(), It.IsAny<Scenario>(), true)).ReturnsAsync((false, scenario));

            var result = await _scenarioController.PutMyScenario(scenario, _SstTowerService.Object, _baseTowerService.Object);

            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }


        [Fact(DisplayName = "Get Scenarios")]
        public async Task GetScenarioIsSuccess()
        {
            IEnumerable<Scenario> scenarios = new List<Scenario>() { new Scenario()
            {
                 Id = 1,
                 ScenarioNo = 1,

                 RecordInsertDateTime = DateTime.UtcNow
            } };

            _configScenarioService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((true, scenarios));

            var configId = "1";
            var result = await _scenarioController.GetScenario(configId, 1);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Get Scenarios no data found")]
        public async Task GetScenarioIsFaild()
        {
            IEnumerable<Scenario> scenarios = new List<Scenario>();

            _configScenarioService.Setup(x => x.GetScenarioAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync((false, scenarios));

            var configId = "1";
            var result = await _scenarioController.GetScenario(configId, 1);
            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }

        [Fact(DisplayName = "Get Scenarios Configuration")]
        public async Task GetScenarioConfigurationIsSuccess()
        {
            IEnumerable<string> configurationList = new List<string>()
            {
                "c12345",
                "C1"
            };

            _configScenarioService.Setup(x => x.GetScenarioConfigurationAsync(It.IsAny<int>())).ReturnsAsync((true, configurationList));

            var result = await _scenarioController.GetScenarioConfiguration(15);

            Assert.Equal(result, configurationList);
            Assert.NotNull(result);
            Assert.IsType<List<string>>(result);
        }

        [Fact(DisplayName = "Get Scenarios Configuration no data found")]
        public async Task GetScenarioConfigurationIsFaild()
        {
            IEnumerable<string> configurationList = new List<string>();

            _configScenarioService.Setup(x => x.GetScenarioConfigurationAsync(It.IsAny<int>())).ReturnsAsync((false, configurationList));

            int quoteId = 11;

            var result = await _scenarioController.GetScenarioConfiguration(quoteId);
            var noResult = new NotFoundObjectResult(result);
            Assert.Equal(404, noResult.StatusCode);
        }


    }

}