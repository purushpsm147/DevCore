using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IOpportunitiesExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<Project>>> GetProjectsAsync(string queryParam = "");
        Task<ExternalServiceResponse<IEnumerable<Project>>> GetProjectsAsync(int? id);
        Task<bool> GetCurrencyLockedDetails(int id);
        Task<ExternalServiceResponse<IEnumerable<Project>>> GetProjectsByUserAsync(string user);
        Task<ExternalServiceResponse<Project>> PutProjectsAsync(Project project);
        Task<ExternalServiceResponse<Project>> PatchProjectsAsync(int id, Project project);

    }
}
