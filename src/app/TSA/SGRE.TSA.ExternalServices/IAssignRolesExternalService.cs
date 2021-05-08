using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface IAssignRolesExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<ProjectRoles>>> PostRolesAsync(IEnumerable<ProjectRoles> projectRoles);
    }
}
