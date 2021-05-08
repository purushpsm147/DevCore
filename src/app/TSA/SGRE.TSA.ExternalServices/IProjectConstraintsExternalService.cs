using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IProjectConstraintsExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<ProjectConstraint>>> GetProjectConstraintsAsync(int projectId);
        Task<ExternalServiceResponse<IEnumerable<ProjectConstraint>>> GetProjectConstraintsByIdAsync(int id);
        Task<ExternalServiceResponse<ProjectConstraint>> PutProjectConstraintsAsync(ProjectConstraint projectConstraint);
        Task<ExternalServiceResponse<ProjectConstraint>> PatchProjectConstraintsAsync(int id, ProjectConstraint projectConstraint);
    }
}
