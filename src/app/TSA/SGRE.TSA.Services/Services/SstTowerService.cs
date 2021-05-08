using Microsoft.Extensions.Logging;
using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class SstTowerService : ISstTowerService
    {
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly IConfigScenarioExternalService _configScenarioExternalService;
        private readonly IConfigScenarioService _configScenarioService;
        private readonly ILogger<SstTowerService> _logger;
        //private readonly ILogger<WtgCatalogue> _WtgCatalogueLogger;

        public SstTowerService(IExternalServiceFactory externalServiceFactory, IConfigScenarioExternalService _configScenarioExternalService, ILogger<SstTowerService> logger, IConfigScenarioService configScenarioService)
        {
            _externalServiceFactory = externalServiceFactory;
            this._configScenarioExternalService = _configScenarioExternalService;
            _logger = logger;
            this._configScenarioService = configScenarioService;
        }
        public async Task<(bool IsSuccess, IEnumerable<SstTower> sstResults)> GetSstTowerIdAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SstTower>(_logger);

            var sstResult = await externalService.GetAsync($"/{id}?$expand=Quote($expand=QuoteLines),WtgCatalogue,loadsCluster,TowerType($expand=noisemodes),SstPredesignDimesions, SstPredesignProposedHubHeights, SstPredesignExistingHubHeights, InitialTower, WtgCatalogueModel");

            if (sstResult.IsSuccess)
            {

                return (true, sstResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<SstTower> sstResults)> GetSstTowerIdNoExpandAsync(int id)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SstTower>(_logger);

            var sstResult = await externalService.GetAsync($"/{id}?$expand=SstPredesignDimesions, SstPredesignProposedHubHeights, SstPredesignExistingHubHeights");

            if (sstResult.IsSuccess)
            {

                return (true, sstResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic sstResults)> PatchSstTowerAsync(int patchId, SstTower sstInput)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SstTower>(_logger);

            var sstInputResult = await externalService.PatchAsync(patchId, sstInput);

            try
            {
                if (sstInputResult.IsSuccess)
                {
                    decimal _aepNomination = 0;
                    decimal _aepOffer = 0;
                    decimal _aepSignature = 0;

                    if (sstInput.IsProposeHubHeight)
                    {
                        _aepNomination = sstInput?.SstPredesignProposedHubHeights.AepNominationGross ?? 0;
                        _aepOffer = sstInput?.SstPredesignProposedHubHeights.AepBindingOfferNet ?? 0;
                        _aepSignature = sstInput?.SstPredesignProposedHubHeights.AepSignatureNet ?? 0;
                    }
                    else
                    {
                        _aepNomination = sstInput?.SstPredesignExistingHubHeights.AepNominationGross ?? 0;
                        _aepOffer = sstInput?.SstPredesignExistingHubHeights.AepBindingOfferNet ?? 0;
                        _aepSignature = sstInput?.SstPredesignExistingHubHeights.AepSignatureNet ?? 0;
                    }

                    ScenarioDTO scenarioDTO = new ScenarioDTO
                    {
                        ItemId = sstInput.Id,
                        QuoteId = sstInput.QuoteId,
                        ScenarioType = ScenarioTypes.SST,
                        WindfarmConfigurationId = sstInput.WindfarmConfigurationId,
                        AepP50NominationGross = _aepNomination,
                        AepP50BindingOfferNet = _aepOffer,
                        AepP50SignatureNet = _aepSignature
                    };

                    var patchScenarioCost = await _configScenarioService.PatchScenarioCostKpiAsync(scenarioDTO);

                    var result = new
                    {
                        project = sstInputResult.ResponseData
                    };
                    return (true, result);
                }

                return (false, sstInputResult.ErrorMessage);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex?.InnerException?.Message);
            }

        }

        public async Task<(bool IsSuccess, dynamic sstResults)> PutSstTowerAsync(SstTower sstTower)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SstTower>(_logger);

            var sstTowerResult = await externalService.PutAsync(sstTower);

            try
            {
                if (sstTowerResult.IsSuccess)
                {
                    Scenario scenario = new Scenario() { };

                    scenario.ItemId = sstTowerResult.ResponseData.Id;
                    scenario.ScenarioType = ScenarioTypes.SST;
                    scenario.Status = ScenarioStatus.Active;
                    scenario.Progress = ScenarioProgress.Nomination;
                    scenario.QuoteId = sstTowerResult.ResponseData.QuoteId;
                    scenario.WindfarmConfigurationId = sstTowerResult.ResponseData.WindfarmConfigurationId;

                    scenario.ScenarioDesigns = new List<ScenarioDesign>()
                    {
                         new ScenarioDesign ()
                         {
                             Risks="-",
                             DesignEvaluation=false
                         }
                    };


                    decimal _aepOfferNet = 0;
                    decimal _aepnominationGross = 0;
                    decimal _aepSignatureNet = 0;

                    if (sstTower.IsProposeHubHeight)
                    {
                        _aepOfferNet = sstTower?.SstPredesignProposedHubHeights.AepBindingOfferNet ?? 0;
                        _aepnominationGross = sstTower?.SstPredesignProposedHubHeights.AepNominationGross ?? 0;
                        _aepSignatureNet = sstTower?.SstPredesignProposedHubHeights.AepSignatureNet ?? 0;

                    }
                    else
                    {
                        _aepOfferNet = sstTower?.SstPredesignExistingHubHeights.AepBindingOfferNet ?? 0;
                        _aepnominationGross = sstTower?.SstPredesignExistingHubHeights.AepNominationGross ?? 0;
                        _aepSignatureNet = sstTower?.SstPredesignExistingHubHeights.AepSignatureNet ?? 0;
                    }

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
                        await externalService.DeleteAsync(sstTowerResult.ResponseData.Id);
                        return (false, scenarioResult.ErrorMessage);
                    }

                    var result = new
                    {
                        project = sstTowerResult.ResponseData
                    };
                    return (true, result);
                }

                return (false, sstTowerResult.ErrorMessage);
            }
            catch (Exception ex)
            {

                _logger?.LogError(ex.ToString());
                return (false, ex?.InnerException?.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<InitialTower> sstResults)> GetIntialTowerAsync(int WtgCatalogueid, int WtgCatalogueModelId, decimal ProposedHubHeight)
        {
            var externalService = _externalServiceFactory.CreateExternalService<WtgCatalogue>(_logger);

            var initialTowerResult = await externalService.GetAsync($"/{WtgCatalogueid}?$expand=initialtowers,WtgCatalogueModels");

            if (initialTowerResult.IsSuccess)
            {
                var wtgCatModel = initialTowerResult.ResponseData.Select(x => x.WtgCatalogueModels.Where(y => y.Id == WtgCatalogueModelId)).FirstOrDefault().Select(x => x.Model).FirstOrDefault();

                IList<InitialTower> initialTowerList = initialTowerResult.ResponseData
                    .Select(x => x.InitialTowers
                    .Where(x => x.HubHeightMinM <= ProposedHubHeight && x.HubHeightMaxM >= ProposedHubHeight && x.Model == wtgCatModel).ToList())
                    .FirstOrDefault();

                return (true, initialTowerList);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic sstResults)> PatchSstTowerWithoutCostKpiAsync(int patchId, SstTower sstInput)
        {
            var externalService = _externalServiceFactory.CreateExternalService<SstTower>(_logger);

            var sstInputResult = await externalService.PatchAsync(patchId, sstInput);

            if (sstInputResult.IsSuccess)
                return (true, sstInputResult.ResponseData);

            return (false, sstInputResult.ErrorMessage);

        }
    }
}
