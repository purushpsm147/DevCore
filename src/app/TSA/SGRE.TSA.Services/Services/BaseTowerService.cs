using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class BaseTowerService : IBaseTowerService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly IConfigScenarioExternalService _configScenarioExternalService;
        private readonly IConfigScenarioService _configScenarioService;
        private readonly ILogger<BaseTowerService> _logger;

        public BaseTowerService(IExternalServiceFactory externalServiceFactory, IConfigScenarioExternalService configScenarioExternalService, ILogger<BaseTowerService> logger, IConfigScenarioService configScenarioService)
        {
            _externalServiceFactory = externalServiceFactory;
            _configScenarioExternalService = configScenarioExternalService;
            _logger = logger;
            this._configScenarioService = configScenarioService;
        }

        public async Task<(bool IsSuccess, IEnumerable<BaseTower> baseTowerResults)> GetBaseTowerAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<BaseTower>(_logger);

            var baseTowerResult = await externalService.GetAsync($"/{id}?$expand=towerType($expand=wtgCatalogue,noisemodes,towersections),loadsCluster,baseTowerDesignDimension,BaseTowerAepHubheight");
            if (baseTowerResult.IsSuccess)
            {
                return (true, baseTowerResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic baseTowerResult)> PutBaseTowerAsync(BaseTower baseTower)
        {
            var externalService = _externalServiceFactory.CreateExternalService<BaseTower>(_logger);

            var baseTowerResult = await externalService.PutAsync(baseTower);
            try
            {
                if (baseTowerResult.IsSuccess)
                {
                    Scenario scenario = new Scenario();

                    scenario.ItemId = baseTowerResult.ResponseData.Id;
                    scenario.ScenarioType = ScenarioTypes.STPETP;
                    scenario.Status = ScenarioStatus.Active;
                    scenario.Progress = ScenarioProgress.Nomination;
                    scenario.QuoteId = baseTowerResult.ResponseData.QuoteId;
                    scenario.WindfarmConfigurationId = baseTowerResult.ResponseData.WindfarmConfigurationId;

                    decimal _aepOfferNet = 0;
                    decimal _aepnominationGross = 0;
                    decimal _aepSignatureNet = 0;

                    _aepOfferNet = baseTower?.BaseTowerAepHubheight?.AepBindingOfferNet ?? 0;
                    _aepnominationGross = baseTower?.BaseTowerAepHubheight?.AepNominationGross ?? 0;
                    _aepSignatureNet = baseTower?.BaseTowerAepHubheight?.AepSignatureNet ?? 0;


                    scenario.ScenarioDesigns = new List<ScenarioDesign>()
                    {
                        new ScenarioDesign ()
                        {
                            Risks="-",
                            DesignEvaluation=false
                        }
                    };

                    scenario.ScenarioCostsKpis = new List<ScenarioCostsKpi>()
                    {
                         new ScenarioCostsKpi ()
                         {
                            Risks="-",

                            TotalCostNomination=0,
                            TotalCostOffer=0,
                            TotalCostSignature=0,

                            TotalTowerExwCostNomination=0,
                            TotalTowerExwCostOffer=0,
                            TotalTowerExwCostSignature=0,

                            AepP50BindingOfferNet=_aepOfferNet,
                            AepP50NominationGross=_aepnominationGross,
                            AepP50SignatureNet=_aepSignatureNet,

                            AepP50Gross=0,
                            AepP50Net=0,
                         }
                    };

                    var scenarioResult = await _configScenarioExternalService.PutScenarioAsync(scenario);

                    if (!scenarioResult.IsSuccess)
                    {
                        await externalService.DeleteAsync(baseTowerResult.ResponseData.Id);
                        return (false, scenarioResult.ErrorMessage);
                    }

                    var result = new
                    {
                        project = baseTowerResult.ResponseData
                    };

                    return (true, result);
                }

                return (false, baseTowerResult.ErrorMessage);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex?.InnerException?.Message);
            }
        }



        public async Task<(bool IsSuccess, dynamic baseTowerResult)> PatchBaseTowerAsync(int patchId, BaseTower baseTower)
        {
            var externalService = _externalServiceFactory.CreateExternalService<BaseTower>(_logger);

            var baseTowerResult = await externalService.PatchAsync(patchId, baseTower);
            try
            {
                if (baseTowerResult.IsSuccess)
                {
                    ScenarioDTO scenarioDTO = new ScenarioDTO
                    {
                        ItemId = baseTower.Id,
                        QuoteId = baseTower.QuoteId,
                        ScenarioType = ScenarioTypes.STPETP,
                        WindfarmConfigurationId = baseTower.WindfarmConfigurationId,
                        AepP50NominationGross = baseTower?.BaseTowerAepHubheight.AepNominationGross ?? 0,
                        AepP50BindingOfferNet = baseTower?.BaseTowerAepHubheight.AepBindingOfferNet ?? 0,
                        AepP50SignatureNet = baseTower?.BaseTowerAepHubheight.AepSignatureNet ?? 0,
                    };

                    var patchScenarioCost = await _configScenarioService.PatchScenarioCostKpiAsync(scenarioDTO);

                    var result = new
                    {
                        project = baseTowerResult.ResponseData
                    };
                    return (true, result);
                }

                return (false, baseTowerResult.ErrorMessage);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex?.InnerException?.Message);
            }
        }

    }
}
