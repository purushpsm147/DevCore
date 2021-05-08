using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class ScenarioController : ControllerBase
    {
        private readonly IConfigScenarioService configScenarioService;

        public ScenarioController(IConfigScenarioService configScenarioService)
        {
            this.configScenarioService = configScenarioService;
        }

        [HttpPut]
        public async Task<IActionResult> PutMyScenario(Scenario scenario, [FromServices] ISstTowerService sstTowerService, [FromServices] IBaseTowerService baseTowerService)
        {
            if (scenario.ScenarioType == ScenarioTypes.SST && scenario.Status == ScenarioStatus.Baseline)
            {
                ModelState.AddModelError("ScenarioStatus", "SST Scenario can not be set as BaseLine status!");
                return BadRequest(ModelState);
            }

            if (scenario.Status == ScenarioStatus.Baseline && await checkBaseLineExist(scenario))
            {
                ModelState.AddModelError("ScenarioStatus", "An Scenario with BaseLine status already exist!");
                return BadRequest(ModelState);
            }

            if (scenario.ScenarioType == ScenarioTypes.SST)
            {
                var sstTowerResult = await sstTowerService.GetSstTowerIdNoExpandAsync(scenario.ItemId);
                if (sstTowerResult.IsSuccess)
                {
                    SstTower sstTower = sstTowerResult.sstResults.FirstOrDefault();

                    sstTower.Id = 0;
                    sstTower.SstUuid = Guid.Empty;

                    if (sstTower.SstPredesignDimesions != null)
                    {
                        sstTower.SstPredesignDimesions.Id = 0;
                        sstTower.SstPredesignDimesions.SstTowerId = 0;
                    }

                    if (sstTower.SstPredesignProposedHubHeights != null)
                    {
                        sstTower.SstPredesignProposedHubHeights.Id = 0;
                        sstTower.SstPredesignProposedHubHeights.SstTowerId = 0;
                    }

                    if (sstTower.SstPredesignExistingHubHeights != null)
                    {
                        sstTower.SstPredesignExistingHubHeights.Id = 0;
                        sstTower.SstPredesignExistingHubHeights.SstTowerId = 0;
                    }

                    sstTower.Quote = null;
                    sstTower.LoadsCluster = null;
                    sstTower.TowerType = null;
                    sstTower.WtgCatalogue = null;

                    //Request Design Evaluation flag details
                    sstTower.IsDesignEvaluationComplete = false;
                    sstTower.IsDesignEvaluationRequest = false;
                    sstTower.RequestDesignEvaluationStartDatetime = null;
                    sstTower.RequestDesignEvaluationEndDatetime = null;

                    var putRes = await sstTowerService.PutSstTowerAsync(sstTower);

                    if (putRes.IsSuccess)
                        return Ok(putRes.sstResults);

                    return NotFound(putRes.sstResults);
                }
            }

            if (scenario.ScenarioType == ScenarioTypes.STPETP)
            {
                var baseTowerResult = await baseTowerService.GetBaseTowerAsync(scenario.ItemId);
                if (baseTowerResult.IsSuccess)
                {
                    BaseTower baseTower = baseTowerResult.baseTowerResults.FirstOrDefault();

                    baseTower.Id = 0;
                    baseTower.BaseTowerDesignDimension.Id = 0;
                    baseTower.LoadsCluster = null;
                    baseTower.LoadsClusterId = 0;
                    baseTower.Quote = null;
                    baseTower.TowerType = null;
                    baseTower.WtgCatalogue = null;

                    baseTower.BaseTowerDesignDimension.BaseTowerId = 0;

                    baseTower.BaseTowerAepHubheight.Id = 0;
                    baseTower.BaseTowerAepHubheight.BaseTowerId = 0;

                    var putRes = await baseTowerService.PutBaseTowerAsync(baseTower);

                    if (putRes.IsSuccess)
                        return Ok(putRes.baseTowerResult);

                    return NotFound(putRes.baseTowerResult);
                }
            }

            return NotFound();
        }


        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchMyScenario(int id, [FromBody] Scenario scenario)
        {
            if (scenario.ScenarioType == ScenarioTypes.SST && scenario.Status == ScenarioStatus.Baseline)
            {
                ModelState.AddModelError("ScenarioStatus", "SST Scenario can not be set as BaseLine status!");
                return BadRequest(ModelState);
            }

            if (scenario.Status == ScenarioStatus.Baseline && await checkBaseLineExist(scenario, id))
            {
                ModelState.AddModelError("ScenarioStatus", "An Scenario with BaseLine status already exist!");
                return BadRequest(ModelState);
            }


            var result = await configScenarioService.PatchScenarioAsync(id, scenario);
            if (result.IsSuccess)
            {
                return Ok(result.scenarioResults);
            }

            return NotFound(result.scenarioResults);
        }

        [HttpGet]
        [Route("configId={configId}&quoteId={quoteId}"), EnableQuery()]
        public async Task<IEnumerable<Scenario>> GetScenario(string configId, int quoteId)
        {
            var result = await configScenarioService.GetScenarioAsync(configId, quoteId);
            if (result.IsSuccess)
            {
                return result.scenarioResults;
            }
            return new List<Scenario>();
        }

        [HttpGet]
        [Route("ScenarioId={ScenarioId}"), EnableQuery()]
        public async Task<IEnumerable<Scenario>> GetScenarioById(int ScenarioId)
        {
            var result = await configScenarioService.GetScenarioByIdAsync(ScenarioId);
            if (result.IsSuccess)
            {
                return result.scenarioResults;
            }
            return new List<Scenario>();
        }

        private async Task<bool> checkBaseLineExist(Scenario scenario)
        {
            var scenarioData = await configScenarioService.GetScenarioAsync(scenario.WindfarmConfigurationId, scenario.QuoteId);

            if (!scenarioData.IsSuccess)
                return false;

            return scenarioData.scenarioResults.Any(s => s.Status == ScenarioStatus.Baseline);
        }

        private async Task<bool> checkBaseLineExist(Scenario scenario, int id)
        {
            var result = await configScenarioService.GetScenarioAsync(scenario.WindfarmConfigurationId, scenario.QuoteId);

            if (!result.IsSuccess)
                return false;

            var baseLineScenarion = result.scenarioResults.FirstOrDefault(s => s.Status == ScenarioStatus.Baseline);
            if (baseLineScenarion != null && baseLineScenarion.Id == id)
            {
                return false;
            }

            if (baseLineScenarion != null && baseLineScenarion.Id != id)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("ConfigurationId/{quoteId:int}"), EnableQuery()]
        public async Task<IEnumerable<string>> GetScenarioConfiguration(int quoteId)
        {
            var result = await configScenarioService.GetScenarioConfigurationAsync(quoteId);
            if (result.IsSuccess)
            {
                return result.scenarioResults;
            }
            return new List<string>();
        }

        [HttpPatch]
        [Route("updateScenarioStatus/{scenarioId:int}")]
        public async Task<IActionResult> PatchScenarioStatus([FromServices] IConfigScenarioService scenarioService, int scenarioId, ScenarioStatus scenarioStatus)
        {
            if (scenarioId <= 0)
                return BadRequest("Scenario Id is missing!");

            var result = await scenarioService.PatchScenarioStatusAsync(scenarioId, scenarioStatus);
            if (result.IsSuccess)
            {
                return Ok(result.scenarioResults);
            }

            return NotFound(result.scenarioResults);
        }

    }
}
