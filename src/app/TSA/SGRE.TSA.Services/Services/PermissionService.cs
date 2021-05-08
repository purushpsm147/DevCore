using SGRE.TSA.ExternalServices;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace SGRE.TSA.Services.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IRoleExternalService roleExternalService;
        private readonly IExternalServiceFactory _externalServiceFactory;
        private readonly ILogger<Permission> _logger;
        public PermissionService(IRoleExternalService roleExternalService, IExternalServiceFactory externalServiceFactory, ILogger<Permission> logger)
        {
            this.roleExternalService = roleExternalService;

            _logger = logger;
            _externalServiceFactory = externalServiceFactory;
        }

        public async Task<(bool IsSuccess, IEnumerable<Permission> permissionResult)> GetPermissionAsync()
        {
            var externalService = _externalServiceFactory.CreateExternalService<Permission>(_logger);

            var permissionResult = await externalService.GetAsync($"?$expand=Role,ProjectModule");

            if (permissionResult.IsSuccess)
            {
                return (true, permissionResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<Permission> permissionResult)> GetPermissionAsync(int roleId)
        {
            var externalService = _externalServiceFactory.CreateExternalService<Permission>(_logger);

            var permissionResult = await externalService.GetAsync($"?$filter=roleId eq {roleId}&$expand=ProjectModule");

            if (permissionResult.IsSuccess)
            {
                return (true, permissionResult.ResponseData);
            }

            return (false, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<Permission> permissionResult)> GetPermissionByRoleNameAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return (false, null);

            ExternalServiceResponse<IEnumerable<Role>> roleResult = await roleExternalService.GetRoleAsync();

            if (!roleResult.IsSuccess)
                return (false, null);

            int _roleId = roleResult.ResponseData.Where(r => r.RoleName.Replace(" ", "") == roleName).Select(r => r.Id).FirstOrDefault();

            var externalService = _externalServiceFactory.CreateExternalService<Permission>(_logger);

            var permissionResult = await externalService.GetAsync($"?$filter=roleId eq {_roleId}&$expand=ProjectModule");


            if (permissionResult.IsSuccess)
            {
                return (true, permissionResult.ResponseData);
            }

            return (false, null);
        }
    }
}
