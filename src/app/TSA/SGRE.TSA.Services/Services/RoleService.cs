using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGRE.TSA.ExternalServices;
using System.Linq;

namespace SGRE.TSA.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleExternalService roleService;
        public RoleService(IRoleExternalService roleService)
        {
            this.roleService = roleService;
        }

        public async Task<(bool IsSuccess, IEnumerable<Role> roleResult)> GetRoleAsync()
        {
            var roleResult = await roleService.GetRoleAsync();
            if (roleResult.IsSuccess)
            {

                return (true, roleResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<PresetRoles> presetRoleResult)> GetPresetRolesAsync()
        {
            var presetRoleResult = await roleService.GetPresetRolesAsync();
            if (presetRoleResult.IsSuccess)
            {

                return (true, presetRoleResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, Role)> GetRoleByNameAsync(string name)
        {
            var roleResult = await roleService.GetRoleAsync();
            if (roleResult.IsSuccess)
            {
                var _role = roleResult.ResponseData.SingleOrDefault(r => r.RoleName.Replace(" ", "") == name);

                return (true, _role);
            }

            return (false, null);
        }
    }
}
