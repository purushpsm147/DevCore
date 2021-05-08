using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IConfigScenarioExternalService
    {
        Task<ExternalServiceResponse<Scenario>> PutScenarioAsync(Scenario scenario);
        Task<ExternalServiceResponse<Scenario>> PatchScenarioAsync(int id, Scenario scenario, bool TriggerAfterEvent = true);
        Task<ExternalServiceResponse<IEnumerable<Scenario>>> GetScenarioAsync(string ConfigId, int quoteId);
        Task<ExternalServiceResponse<IEnumerable<string>>> GetScenarioConfigurationAsync(int quoteId);
        Task<ExternalServiceResponse<IEnumerable<Scenario>>> GetScenarioByIdAsync(int scenarioId);
        Task<ExternalServiceResponse<IEnumerable<ScenarioOverView>>> GetScenarioOverViewByScenarioIdAsync(int scenarioId);
        Task<ExternalServiceResponse<Scenario>> PatchScenarioStatusAsync(int scenarioId, ScenarioStatus scenarioStatus);
    }
}
