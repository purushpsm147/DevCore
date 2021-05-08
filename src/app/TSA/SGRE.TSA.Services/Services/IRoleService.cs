using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IRoleService
    {
        Task<(bool IsSuccess, IEnumerable<Role> roleResult)> GetRoleAsync();
        Task<(bool IsSuccess, IEnumerable<PresetRoles> presetRoleResult)> GetPresetRolesAsync();
        Task<(bool IsSuccess, Role)> GetRoleByNameAsync(string name);
    }
}
