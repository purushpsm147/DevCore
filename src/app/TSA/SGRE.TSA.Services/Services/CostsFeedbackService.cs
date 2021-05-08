using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class CostsFeedbackService : ICostsFeedbackService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly IConfigScenarioService _configScenarioService;
        private readonly ILogger<CostsFeedbackService> _logger;

        public CostsFeedbackService(IExternalServiceFactory externalServiceFactory, ILogger<CostsFeedbackService> logger, IConfigScenarioService configScenarioService)
        {
            this._externalServiceFactory = externalServiceFactory;
            this._logger = logger;
            this._configScenarioService = configScenarioService;
        }

        public async Task<(bool IsSuccess, IEnumerable<CostFeedback> costFeebackResults)> GetCostFeeback(int scenarioId)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostFeedback>(_logger);

            var sstResult = await externalService.GetByIdAsync(scenarioId);

            if (sstResult.IsSuccess)
            {

                return (true, sstResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<CostOverView> costOverViewResults)> GetCostOverView(int scenarioId)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostOverView>(_logger);

            var rnaExternalService = _externalServiceFactory.CreateExternalService<CostOverViewRNA>(_logger);

            var sstResult = await externalService.GetAsync($"?$Filter=scenarioId eq {scenarioId}");


            if (sstResult.IsSuccess)
            {
                List<CostOverView> sstResultList = sstResult.ResponseData.ToList();

                if (sstResultList.Count < 7)
                {
                    var sstResultRNA = await rnaExternalService.GetAsync($"?$Filter=scenarioId eq {scenarioId}");
                    if (sstResultRNA.IsSuccess)
                    {
                        List<CostOverView> result = JsonConvert.DeserializeObject<List<CostOverView>>(JsonConvert.SerializeObject(sstResultRNA.ResponseData));

                        return (true, result);
                    }
                    else
                    {
                        return (false, null);
                    }
                }
                return (true, sstResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic costFeebackResults)> PatchCostFeebackAsync(int id, CostFeedback costFeeback)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostFeedback>(_logger);

            var costFeebackResult = await externalService.PatchAsync(id, costFeeback);

            if (costFeebackResult.IsSuccess)
            {

                var viewResult = await GetCostOverView(costFeeback.ScenarioId);
                if (viewResult.IsSuccess)
                {
                    var viewData = viewResult.costOverViewResults;

                    var scenarioResult = await _configScenarioService.GetScenarioByIdAsync(costFeeback.ScenarioId);

                    if (scenarioResult.IsSuccess)
                    {
                        Scenario scenario = scenarioResult.scenarioResults.FirstOrDefault();

                        var totalCosts = (from v in viewData where v.PositionId == 9 select new { v.NominationWindfarm, v.OfferWindfarm, v.SignatureWindfarm }).FirstOrDefault();

                        //Assigned Total Cost to TowerExWorks Column - Address column name Changes in the Next Release
                        var towerExWorks = (from v in viewData where v.PositionId == 7 select new { v.NominationWindfarm, v.OfferWindfarm, v.SignatureWindfarm }).FirstOrDefault();

                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalCostNomination = totalCosts.NominationWindfarm;
                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalCostOffer = totalCosts.OfferWindfarm;
                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalCostSignature = totalCosts.SignatureWindfarm;

                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalTowerExwCostNomination = towerExWorks.NominationWindfarm;
                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalTowerExwCostOffer = towerExWorks.OfferWindfarm;
                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalTowerExwCostSignature = towerExWorks.SignatureWindfarm;

                        scenario.Quote = null;
                        scenario.wtgCatalogue = null;

                        var scenarioPatchResult = await _configScenarioService.PatchScenarioAsync(costFeeback.ScenarioId, scenario, false);

                    }
                }

                var result = new
                {
                    costOverView = costFeebackResult.ResponseData
                };
                return (true, result);
            }

            return (false, costFeebackResult.ErrorMessage);
        }

        public async Task<(bool IsSuccess, dynamic costFeebackResults)> PutCostFeebackAsync(CostFeedback costFeeback)
        {
            var externalService = _externalServiceFactory.CreateExternalService<CostFeedback>(_logger);

            var costFeebackResult = await externalService.PutAsync(costFeeback);

            if (costFeebackResult.IsSuccess)
            {

                var viewResult = await GetCostOverView(costFeeback.ScenarioId);
                if (viewResult.IsSuccess)
                {
                    var viewData = viewResult.costOverViewResults;

                    //Getting scenario data
                    var scenarioResult = await _configScenarioService.GetScenarioByIdAsync(costFeeback.ScenarioId);

                    if (scenarioResult.IsSuccess)
                    {
                        Scenario scenario = scenarioResult.scenarioResults.FirstOrDefault();

                        var totalCosts = (from v in viewData where v.PositionId == 9 select new { v.NominationWindfarm, v.OfferWindfarm, v.SignatureWindfarm }).FirstOrDefault();
                        //Assigned Total Cost to TowerExWorks Column - Address column name Changes in the Next Release
                        var towerExWorks = (from v in viewData where v.PositionId == 7 select new { v.NominationWindfarm, v.OfferWindfarm, v.SignatureWindfarm }).FirstOrDefault();

                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalCostNomination = totalCosts.NominationWindfarm;
                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalCostOffer = totalCosts.OfferWindfarm;
                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalCostSignature = totalCosts.SignatureWindfarm;

                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalTowerExwCostNomination = towerExWorks.NominationWindfarm;
                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalTowerExwCostOffer = towerExWorks.OfferWindfarm;
                        scenario.ScenarioCostsKpis.FirstOrDefault().TotalTowerExwCostSignature = towerExWorks.SignatureWindfarm;

                        scenario.Quote = null;
                        scenario.wtgCatalogue = null;

                        var scenarioPatchResult = await _configScenarioService.PatchScenarioAsync(costFeeback.ScenarioId, scenario, false);

                    }
                }

                var result = new
                {
                    costFeedback = costFeebackResult.ResponseData
                };
                return (true, result);
            }

            return (false, costFeebackResult.ErrorMessage);
        }
    }
}
