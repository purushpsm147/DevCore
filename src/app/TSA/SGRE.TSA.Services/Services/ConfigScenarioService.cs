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
    public class ConfigScenarioService : IConfigScenarioService
    {
        private readonly IConfigScenarioExternalService configScenarioExternalService;
        private readonly ILogger<ConfigScenarioService> logger;

        public ConfigScenarioService(IConfigScenarioExternalService configScenarioExternalService, ILogger<ConfigScenarioService> logger)
        {
            this.configScenarioExternalService = configScenarioExternalService;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Scenario> scenarioResults)> GetScenarioAsync(string ConfigId, int quoteId)
        {
            var scenarioResult = await configScenarioExternalService.GetScenarioAsync(ConfigId, quoteId);

            try
            {
                if (scenarioResult.IsSuccess)
                {
                    IEnumerable<Scenario> scenarioData = scenarioResult.ResponseData;

                    if (scenarioData.Any(s => s.Status == ScenarioStatus.Baseline))
                    {
                        var baseLineData = (from s in scenarioData
                                            where s.Status == ScenarioStatus.Baseline
                                            from c in s.ScenarioCostsKpis
                                            select new
                                            {
                                                baseLine_coe = c.Coe,
                                                baseLine_TotalCost = c.TotalTowerExwCost,
                                                baseLine_Aep = c.AepP50Gross,
                                            }).FirstOrDefault();




                        scenarioData = from s in scenarioData
                                       from c in s.ScenarioCostsKpis
                                       .Select(ii =>
                                       {
                                           if (s.Status == ScenarioStatus.Baseline)
                                           {
                                               ii.CoeDelta = 100;
                                               ii.CapexDelta = 100;
                                               ii.AepP50Delta = 100;
                                           }
                                           else
                                           {
                                               if (ii.TotalTowerExwCost == 0)
                                               {
                                                   if (ii.AepP50Gross == 0)
                                                   {
                                                       ii.CoeDelta = 0;
                                                       ii.CapexDelta = 0;
                                                       ii.AepP50Delta = 0;
                                                   }
                                                   else
                                                   {
                                                       ii.CoeDelta = 0;
                                                       ii.CapexDelta = 0;
                                                       ii.AepP50Delta = (baseLineData.baseLine_Aep != 0) ? Math.Round(((ii.AepP50Gross / baseLineData.baseLine_Aep) - 1) * 100, 3) : 0;
                                                   }
                                               }
                                               else if (ii.AepP50Gross == 0)
                                               {
                                                   ii.CoeDelta = 0;
                                                   ii.CapexDelta = (baseLineData.baseLine_TotalCost != 0) ? Math.Round((ii.TotalTowerExwCost - baseLineData.baseLine_TotalCost), 3) : 0;
                                                   ii.AepP50Delta = 0;
                                               }
                                               else
                                               {
                                                   ii.CoeDelta = (baseLineData.baseLine_coe != 0) ? Math.Round(((ii.Coe / baseLineData.baseLine_coe) - 1) * 100, 3) : 0;

                                                   ii.CapexDelta = (baseLineData.baseLine_TotalCost != 0) ? Math.Round((ii.TotalTowerExwCost - baseLineData.baseLine_TotalCost), 3) : 0;

                                                   ii.AepP50Delta = (baseLineData.baseLine_Aep != 0) ? Math.Round(((ii.AepP50Gross / baseLineData.baseLine_Aep) - 1) * 100, 3) : 0;
                                               }
                                           }
                                           return ii;
                                       })
                                       select s;
                    }
                    else
                    {
                        scenarioData = from s in scenarioData
                                       from c in s.ScenarioCostsKpis
                                       .Select(ii =>
                                       {
                                           ii.CoeDelta = 0;
                                           ii.CapexDelta = 0;
                                           ii.AepP50Delta = 0;
                                           return ii;
                                       })
                                       select s;
                    }

                    return (true, scenarioData);
                }
                return (false, null);

            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null);
            }
        }


        public async Task<(bool IsSuccess, IEnumerable<string> scenarioResults)> GetScenarioConfigurationAsync(int quoteId)
        {
            var scenarioResult = await configScenarioExternalService.GetScenarioConfigurationAsync(quoteId);

            if (scenarioResult.IsSuccess)
            {
                return (true, scenarioResult.ResponseData);
            }
            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic scenarioResults)> PatchScenarioAsync(int id, Scenario scenario, bool TriggerAfterEvent = true)
        {
            var scenarioResult = await configScenarioExternalService.PatchScenarioAsync(id, scenario, TriggerAfterEvent);
            if (scenarioResult.IsSuccess)
            {
                var result = new
                {
                    project = scenarioResult.ResponseData
                };
                return (true, result);
            }

            return (false, scenarioResult.ErrorMessage);
        }

        public async Task<(bool IsSuccess, dynamic scenarioResults)> PutScenarioAsync(Scenario scenario)
        {
            var scenarioResult = await configScenarioExternalService.PutScenarioAsync(scenario);
            if (scenarioResult.IsSuccess)
            {
                var result = new
                {
                    project = scenarioResult.ResponseData
                };
                return (true, result);
            }

            return (false, scenarioResult.ErrorMessage);
        }

        public async Task<(bool IsSuccess, IEnumerable<Scenario> scenarioResults)> GetScenarioByIdAsync(int scenarioId)
        {
            var scenarioResult = await configScenarioExternalService.GetScenarioByIdAsync(scenarioId);
            if (scenarioResult.IsSuccess)
            {
                return (true, scenarioResult.ResponseData);
            }
            return (false, null);
        }
        public async Task<(bool IsSuccess, IEnumerable<ScenarioOverView> scenarioOverViewResults)> GetScenarioOverViewByScenarioIdAsync(int scenarioId)
        {
            var scenarioOverViewResult = await configScenarioExternalService.GetScenarioOverViewByScenarioIdAsync(scenarioId);
            if (scenarioOverViewResult.IsSuccess)
            {
                return (true, scenarioOverViewResult.ResponseData);
            }
            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic scenarioCostKpiResults)> PatchScenarioCostKpiAsync(ScenarioDTO scenarioDTO)
        {
            var scenarioResult = await configScenarioExternalService.GetScenarioAsync(scenarioDTO.WindfarmConfigurationId, scenarioDTO.QuoteId);

            try
            {
                if (scenarioResult.IsSuccess && scenarioResult.ResponseData.Any())
                {
                    var scenarioData = scenarioResult.ResponseData.ToList();

                    var filterdScenario = (from sd in scenarioData where sd.ItemId == scenarioDTO.ItemId & sd.ScenarioType == scenarioDTO.ScenarioType select sd).FirstOrDefault();

                    filterdScenario.loadCluster = null;
                    filterdScenario.Quote = null;

                    filterdScenario.ScenarioCostsKpis.FirstOrDefault().AepP50NominationGross = scenarioDTO.AepP50NominationGross;
                    filterdScenario.ScenarioCostsKpis.FirstOrDefault().AepP50BindingOfferNet = scenarioDTO.AepP50BindingOfferNet;
                    filterdScenario.ScenarioCostsKpis.FirstOrDefault().AepP50SignatureNet = scenarioDTO.AepP50SignatureNet;

                    var patchScenarioResult = await configScenarioExternalService.PatchScenarioAsync(filterdScenario.Id, filterdScenario, false);
                    if (patchScenarioResult.IsSuccess)
                    {
                        var result = new
                        {
                            project = patchScenarioResult.ResponseData
                        };
                        return (true, result);
                    }
                    else
                    {
                        scenarioResult.ErrorMessage = patchScenarioResult.ErrorMessage;
                    }
                }

                return (false, scenarioResult.ErrorMessage);
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, ex?.InnerException?.Message);
            }
        }

        public async Task<(bool IsSuccess, dynamic scenarioResults)> PatchScenarioStatusAsync(int scenarioId, ScenarioStatus scenarioStatus)
        {
            var scenarioResult = await configScenarioExternalService.PatchScenarioStatusAsync(scenarioId, scenarioStatus);
            if (scenarioResult.IsSuccess)
            {
                var result = new
                {
                    scenario = scenarioResult.ResponseData
                };
                return (true, result);
            }

            return (false, scenarioResult.ErrorMessage);
        }
    }

}
