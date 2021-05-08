using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public class ConfigScenarioExternalService : IConfigScenarioExternalService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ConfigScenarioExternalService> logger;

        public ConfigScenarioExternalService(IHttpClientFactory httpClientFactory, ILogger<ConfigScenarioExternalService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<ExternalServiceResponse<Scenario>> PatchScenarioAsync(int id, Scenario scenario, bool TriggerAfterEvent = true)
        {
            try
            {
                //TODO: Can Scenario status only in Patch
                var client = httpClientFactory.CreateClient("ToSAService");

                var jsonString = JsonConvert.SerializeObject(scenario, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                if (TriggerAfterEvent)
                    client.DefaultRequestHeaders.Add("TriggerAfterEvent", TriggerAfterEvent ? "true" : "false");

                var response = await client.PatchAsync($"api/Scenario/{id}", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<Scenario>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = scenario
                    };
                }

                return new ExternalServiceResponse<Scenario>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Scenario>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<ExternalServiceResponse<Scenario>> PutScenarioAsync(Scenario scenario)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                int lastdbScenarioNo = await GetLastScenarioNo();
                if (lastdbScenarioNo < 0)
                {
                    return new ExternalServiceResponse<Scenario>()
                    {
                        IsSuccess = false,
                        ErrorMessage = "Server error on scenario no",
                        ResponseData = null
                    };
                }

                scenario.ScenarioNo = lastdbScenarioNo + 1;

                var jsonString = JsonConvert.SerializeObject(scenario, Formatting.Indented);

                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"api/Scenario", httpContent);

                var exceptionDetails = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return new ExternalServiceResponse<Scenario>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = scenario
                    };
                }

                return new ExternalServiceResponse<Scenario>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Scenario>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<Scenario>>> GetScenarioAsync(string ConfigId, int quoteId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/ScenarioOverView?$filter=WindfarmConfigurationId eq '{ConfigId}' and quoteId eq  {quoteId} &$expand=WtgCatalogue,ScenarioDesign,ScenarioCostKpi,loadsCluster");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ScenarioOverView>>(content, options);

                    List<Scenario> scenarioList = new List<Scenario>();
                    foreach (var item in result)
                    {
                        decimal _totalCost = 0;
                        decimal _totalCostExw = 0;
                        decimal _aepGross = 0;

                        decimal _coe = 0;



                        if (item.Progress == Models.Enums.ScenarioProgress.Nomination)
                        {
                            _totalCost = item.ScenarioCostKpi.TotalCostNomination;
                            _totalCostExw = item.ScenarioCostKpi.TotalTowerExwCostNomination;
                            _aepGross = item.ScenarioCostKpi.AepP50NominationGross;
                        }

                        if (item.Progress == Models.Enums.ScenarioProgress.Offer)
                        {
                            _totalCost = item.ScenarioCostKpi.TotalCostOffer;
                            _totalCostExw = item.ScenarioCostKpi.TotalTowerExwCostOffer;
                            _aepGross = item.ScenarioCostKpi.AepP50BindingOfferNet;
                        }

                        if (item.Progress == Models.Enums.ScenarioProgress.Signature)
                        {
                            _totalCost = item.ScenarioCostKpi.TotalCostSignature;
                            _totalCostExw = item.ScenarioCostKpi.TotalTowerExwCostSignature;
                            _aepGross = item.ScenarioCostKpi.AepP50SignatureNet;
                        }


                        if (_totalCost > 0 && _aepGross > 0)
                            _coe = Math.Round((_totalCost / _aepGross), 3);

                        scenarioList.Add(new Scenario
                        {
                            Id = item.ScenarioId,
                            ScenarioNo = item.ScenarioNo,
                            wtgCatalogue = item.wtgCatalogue,
                            StepProgress = item.StepProgress,
                            loadCluster = item.loadsCluster.ClusterName,
                            Options = item.Options,
                            Progress = item.Progress,
                            ItemId = item.ItemId,
                            QuoteId = item.QuoteId,
                            ScenarioType = item.ScenarioType,
                            Status = item.Status,
                            WindfarmConfigurationId = item.WindfarmConfigurationId,

                            WtgCatalogueId = item.WtgCatalogueId,
                            DesignEvaluationRisk = item.DesignEvaluationRisk,

                            ScenarioDesigns = new List<ScenarioDesign>()
                            {
                                new ScenarioDesign()
                                {
                                    Id = item.ScenarioDesign.Id,
                                    ScenarioId = item.ScenarioDesign.ScenarioId,
                                    LifeTime = item.LifeTime,
                                    TowerWeight = item.TowerWeight,
                                    Risks = item.ScenarioDesign.Risks,
                                    DesignEvaluation = item.ScenarioDesign.DesignEvaluation,
                                    StepProgress = item.StepProgress,
                                    HubHeight = item.HubHeight
                                }
                            },

                            ScenarioCostsKpis = new List<ScenarioCostsKpi>()
                            {

                               new ScenarioCostsKpi()
                               {
                                   //expose logic will be here (delta calculation).. @anil
                                   Id=item.ScenarioCostKpi.Id,
                                   ScenarioId=item.ScenarioCostKpi.ScenarioId,

                                   //delta calculation part start.....

                                   TotalCost=_totalCost,
                                   TotalTowerExwCost=_totalCostExw,

                                   AepP50Gross=_aepGross,
                                   Coe=_coe,


                                   TotalCostNomination=item.ScenarioCostKpi.TotalCostNomination,
                                   TotalCostSignature=item.ScenarioCostKpi.TotalCostSignature,
                                   TotalCostOffer=item.ScenarioCostKpi.TotalCostOffer,

                                   TotalTowerExwCostNomination=item.ScenarioCostKpi.TotalTowerExwCostNomination,
                                   TotalTowerExwCostOffer=item.ScenarioCostKpi.TotalTowerExwCostOffer,
                                   TotalTowerExwCostSignature=item.ScenarioCostKpi.TotalTowerExwCostSignature,

                                   AepP50BindingOfferNet=item.ScenarioCostKpi.AepP50BindingOfferNet,
                                   AepP50NominationGross=item.ScenarioCostKpi.AepP50NominationGross,
                                   AepP50SignatureNet=item.ScenarioCostKpi.AepP50SignatureNet,


                               }
                            }

                        });

                    }

                    return new ExternalServiceResponse<IEnumerable<Scenario>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = scenarioList
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Scenario>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Scenario>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        private async Task<int> GetLastScenarioNo()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Scenario?$orderby=ScenarioNo desc&$top=1");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Scenario>>(content, options);

                    if (result != null && result.Any())
                        return result.FirstOrDefault().ScenarioNo;
                    else return 0;
                }

                return -1;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return -1;
            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<string>>> GetScenarioConfigurationAsync(int quoteId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Scenario?$filter=quoteId eq {quoteId} & $select=windfarmConfigurationId");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var scenarioResult = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Scenario>>(content, options);

                    var filteredConfiguration = (from sr in scenarioResult select sr.WindfarmConfigurationId).Distinct().ToList();

                    return new ExternalServiceResponse<IEnumerable<string>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = filteredConfiguration
                    };
                }

                return new ExternalServiceResponse<IEnumerable<string>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<string>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<Scenario>>> GetScenarioByIdAsync(int scenarioId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/Scenario?$filter=id eq {scenarioId}&$expand=quote($expand=Proposal,QuoteLines),scenarioDesigns,scenarioCostsKpis");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Scenario>>(content, options);

                    return new ExternalServiceResponse<IEnumerable<Scenario>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<IEnumerable<Scenario>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<Scenario>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<ExternalServiceResponse<IEnumerable<ScenarioOverView>>> GetScenarioOverViewByScenarioIdAsync(int scenarioId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.GetAsync($"api/ScenarioOverView?$filter=ScenarioId eq {scenarioId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ScenarioOverView>>(content, options);

                    List<ScenarioOverView> scenarioOverviewList = new List<ScenarioOverView>();

                    return new ExternalServiceResponse<IEnumerable<ScenarioOverView>>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = scenarioOverviewList
                    };
                }

                return new ExternalServiceResponse<IEnumerable<ScenarioOverView>>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<IEnumerable<ScenarioOverView>>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<ExternalServiceResponse<Scenario>> PatchScenarioStatusAsync(int scenarioId, ScenarioStatus scenarioStatus)
        {
            try
            {
                var client = httpClientFactory.CreateClient("ToSAService");

                var response = await client.PatchAsync($"api/Scenario/updateScenarioStatus/scenarioId={scenarioId}&scenarioStatus={scenarioStatus}", null);

                var exceptionDetails = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<Scenario>(content, options);

                    return new ExternalServiceResponse<Scenario>()
                    {
                        IsSuccess = true,
                        ErrorMessage = null,
                        ResponseData = result
                    };
                }

                return new ExternalServiceResponse<Scenario>()
                {
                    IsSuccess = false,
                    ErrorMessage = response.ReasonPhrase + "\n" + exceptionDetails,
                    ResponseData = null
                };
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return new ExternalServiceResponse<Scenario>()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ResponseData = null
                };
            }
        }
    }
}
