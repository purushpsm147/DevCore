using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IConfigScenarioService
    {
        Task<(bool IsSuccess, IEnumerable<Scenario> scenarioResults)> GetScenarioAsync(string ConfigId, int quoteId);
        Task<(bool IsSuccess, dynamic scenarioResults)> PutScenarioAsync(Scenario scenario);
        Task<(bool IsSuccess, dynamic scenarioResults)> PatchScenarioAsync(int id, Scenario scenario, bool TriggerAfterEvent = true);
        Task<(bool IsSuccess, IEnumerable<string> scenarioResults)> GetScenarioConfigurationAsync(int quoteId);
        Task<(bool IsSuccess, IEnumerable<Scenario> scenarioResults)> GetScenarioByIdAsync(int scenarioId);
        Task<(bool IsSuccess, IEnumerable<ScenarioOverView> scenarioOverViewResults)> GetScenarioOverViewByScenarioIdAsync(int scenarioId);
        Task<(bool IsSuccess, dynamic scenarioCostKpiResults)> PatchScenarioCostKpiAsync(ScenarioDTO scenarioDTO);
        Task<(bool IsSuccess, dynamic scenarioResults)> PatchScenarioStatusAsync(int scenarioId, ScenarioStatus scenarioStatus);
    }
}
