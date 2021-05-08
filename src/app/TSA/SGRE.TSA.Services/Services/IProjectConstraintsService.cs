using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SGRE.TSA.Services.Services
{
    public interface IProjectConstraintsService
    {
        Task<(bool IsSuccess, IEnumerable<ProjectConstraint> ConstraintResults)> GetProjectConstraintsAsync(int projectId);
        Task<(bool IsSuccess, IEnumerable<ProjectConstraint> ConstraintResults)> GetProjectConstraintsByIdAsync(int id);
        Task<(bool IsSuccess, dynamic ConstraintResults)> PutProjectConstraintsAsync(ProjectConstraint projectConstraint);
        Task<(bool IsSuccess, dynamic ConstraintResults)> PatchProjectConstraintsAsync(int id, ProjectConstraint projectConstraint);
    }
}
