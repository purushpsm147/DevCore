using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models.Enums;
using SGRE.TSA.Services.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class MyAccessController : ControllerBase
    {
        public record UserAccess(string ResourceName, PermissionTypes access);

        public record RoleInfo(int? RoleId, string CurrentRole);

        [HttpGet, Route("{Resource}"), EnableQuery()]
        public async Task<IEnumerable<UserAccess>> GetMyAccess([FromServices] IPermissionService permissionService, string Resource, ODataQueryOptions queryOpions)
        {
            var role = User.Claims.FirstOrDefault(cl => cl.Type.Contains("role")).Value;
            if (role == null)
            {
                HttpContext.Response.StatusCode = 403;
                return null;
            }
            var Result = await permissionService.GetPermissionByRoleNameAsync(role);

            if (Result.IsSuccess)
            {
                return from p in Result.permissionResult
                       join k in ControllerMaps.GetControllerModuleMaps()
                       on p.ProjectModule.ModuleName equals k.ModuleName
                       where k.ResourceName == Resource
                       select new UserAccess(p.ProjectModule.ModuleName, p.PermissionType);
            }

            return new List<UserAccess>();
        }

        [HttpGet, Route("MyRole")]
        public async Task<RoleInfo> GetMyRole([FromServices] IRoleService roleService)
        {
            var role = User.Claims.FirstOrDefault(cl => cl.Type.Contains("role")).Value;
            if (role == null)
            {
                HttpContext.Response.StatusCode = 403;
                return null;
            }
            var internalRole = await roleService.GetRoleByNameAsync(role);

            return new RoleInfo(internalRole.Item2?.Id, role);
        }
    }
}
