using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public class AssignRolesService : IAssignRolesService
    {
        private readonly IAssignRolesExternalService assignRoles;

        public AssignRolesService(IAssignRolesExternalService assignRoles)
        {
            this.assignRoles = assignRoles;
        }
        public async Task<(bool IsSuccess, IEnumerable<ProjectRoles> rolesAdded)> PostRolesAsync(IEnumerable<ProjectRoles> projectRoles)
        {
            var assignRolesResults = await assignRoles.PostRolesAsync(projectRoles);
            if (assignRolesResults.IsSuccess)
            {
                return (true, assignRolesResults.ResponseData);
            }

            return (false, null);
        }
    }
}
