using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class ProjectConstraintsService : IProjectConstraintsService
    {
        private readonly IProjectConstraintsExternalService projectConstraintsExternalService;

        public ProjectConstraintsService(IProjectConstraintsExternalService projectConstraintsExternalService)
        {
            this.projectConstraintsExternalService = projectConstraintsExternalService;
        }

        public async Task<(bool IsSuccess, IEnumerable<ProjectConstraint> ConstraintResults)> GetProjectConstraintsAsync(int projectId)
        {
            var constraintsResult = await projectConstraintsExternalService.GetProjectConstraintsAsync(projectId);
            if (constraintsResult.IsSuccess)
            {
                var result = constraintsResult.ResponseData;
                return (true, result);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<ProjectConstraint> ConstraintResults)> GetProjectConstraintsByIdAsync(int id)
        {
            var constraintsResult = await projectConstraintsExternalService.GetProjectConstraintsByIdAsync(id);
            if (constraintsResult.IsSuccess)
            {
                var result = constraintsResult.ResponseData;
                return (true, result);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, dynamic ConstraintResults)> PatchProjectConstraintsAsync(int id, ProjectConstraint projectConstraint)
        {
            var constraintResult = await projectConstraintsExternalService.PatchProjectConstraintsAsync(id, projectConstraint);
            if (constraintResult.IsSuccess)
            {
                var result = new
                {
                    project = constraintResult.ResponseData
                };
                return (true, result);
            }

            return (false, constraintResult.ErrorMessage);
        }

        public async Task<(bool IsSuccess, dynamic ConstraintResults)> PutProjectConstraintsAsync(ProjectConstraint projectConstraint)
        {
            var constraintResult = await projectConstraintsExternalService.PutProjectConstraintsAsync(projectConstraint);
            if (constraintResult.IsSuccess)
            {
                var result = new
                {
                    project = constraintResult.ResponseData
                };
                return (true, result);
            }

            return (false, constraintResult.ErrorMessage);
        }
    }
}
