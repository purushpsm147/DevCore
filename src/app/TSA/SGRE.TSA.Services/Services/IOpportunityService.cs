using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{

    /// <summary>
    /// Will pass this interface to return Opportunity results
    /// relevant to the UserId. The return type is ValuedTuple with
    /// API status and API Json results
    /// </summary>
    public interface IOpportunityService
    {
     
        Task<(bool IsSuccess, IEnumerable<Project> OpportunityResults)> SearchOpportunityAsync();

        Task<(bool IsSuccess, IEnumerable<Project> OpportunityResults)> GetMyOpportunityAsync(string user);
        Task<(bool IsSuccess, IEnumerable<Project> OpportunityResults)> GetOpportunityByIdAsync(int id);
        Task<bool> GetCurrencyLockedDetails(int id);

        Task<(bool IsSuccess, dynamic opportunityResults)> PutProjectsAsync(Project project);
        Task<(bool IsSuccess, dynamic opportunityResults)> PatchProjectsAsync(int id, Project project);
    }
}
