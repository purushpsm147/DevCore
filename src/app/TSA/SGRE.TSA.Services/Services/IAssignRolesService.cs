using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IAssignRolesService
    {
        Task<(bool IsSuccess, IEnumerable<ProjectRoles> rolesAdded)> PostRolesAsync(IEnumerable<ProjectRoles> projectRoles);
    }
}
