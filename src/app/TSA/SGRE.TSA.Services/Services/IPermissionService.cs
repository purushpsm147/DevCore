using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface IPermissionService
    {
        Task<(bool IsSuccess, IEnumerable<Permission> permissionResult)> GetPermissionAsync();
        Task<(bool IsSuccess, IEnumerable<Permission> permissionResult)> GetPermissionAsync(int roleId);
        Task<(bool IsSuccess, IEnumerable<Permission> permissionResult)> GetPermissionByRoleNameAsync(string roleName);
    }
}
