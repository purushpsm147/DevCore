using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class OpportunityService : IOpportunityService
    {
        private readonly IOpportunitiesExternalService opportunityExternalService;

        public OpportunityService(IOpportunitiesExternalService opportunityExternalService)
        {
            this.opportunityExternalService = opportunityExternalService;
        }

        public async Task<(bool IsSuccess, IEnumerable<Project> OpportunityResults)> SearchOpportunityAsync()
        {
            var projectsResult = await opportunityExternalService.GetProjectsAsync();
            if (projectsResult.IsSuccess)
            {
                var result = projectsResult.ResponseData;
                return (true, result);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<Project> OpportunityResults)> GetMyOpportunityAsync(string user)
        {
            var projectsResult = await opportunityExternalService.GetProjectsByUserAsync(user);
            if (projectsResult.IsSuccess)
            {
                return (true, projectsResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic opportunityResults)> PutProjectsAsync(Project project)
        {
            var projectResult = await opportunityExternalService.PutProjectsAsync(project);
            if (projectResult.IsSuccess)
            {
                var result = new
                {
                    project = projectResult.ResponseData
                };
                return (true, result);
            }

            return (false, projectResult.ErrorMessage);
        }
        public async Task<(bool IsSuccess, dynamic opportunityResults)> PatchProjectsAsync(int id, Project project)
        {
            var projectResult = await opportunityExternalService.PatchProjectsAsync(id, project);
            if (projectResult.IsSuccess)
            {
                var result = new
                {
                    project = projectResult.ResponseData
                };
                return (true, result);
            }

            return (false, projectResult.ErrorMessage);
        }

        public async Task<(bool IsSuccess, IEnumerable<Project> OpportunityResults)> GetOpportunityByIdAsync(int id)
        {
            var projectResult = await opportunityExternalService.GetProjectsAsync(id);
            if(projectResult.IsSuccess)
            {
                return (true, projectResult.ResponseData);
            }
            return (false, null);
        }

        public async Task<bool> GetCurrencyLockedDetails(int id)
        {
            var currencyLockResult = await opportunityExternalService.GetCurrencyLockedDetails(id);
            if (currencyLockResult)
            {
                return true;
            }
            return false;
        }
    }
}
