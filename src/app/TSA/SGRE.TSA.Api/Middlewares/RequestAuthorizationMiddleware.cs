using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace SGRE.TSA.Api.Middlewares
{
    public class RequestAuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMemoryCache memoryCache;
        public RequestAuthorizationMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            this.next = next;
            this.memoryCache = memoryCache;
        }

        private IEnumerable<string> GetExemptedControllers()
        {
            return new List<string>() { "meta", "myaccess", "costsfeedback", "basetower", "designevaluation", "ssttower", "fileupload", "aeplookup", "support", "scenario" };
        }

        public async Task InvokeAsync(HttpContext context, [FromServices] IPermissionService permissionService)
        {
            try
            {
                var requestType = context.Request.Method;
                var controller = context.Request.Path.Value.Replace("/api/v1/", "").Split('/')[0];

                if (GetExemptedControllers().Contains(controller.ToLower()))
                {
                    await next(context);
                    return;
                }

                if (context.User.Claims.Count() == 0)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                var roles = context.User.Claims.FirstOrDefault(cl => cl.Type.Contains("role")).Value;

                if (roles == null)
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                PermissionTypes typeRequired = requestType == "GET" ? PermissionTypes.Read : PermissionTypes.Write;

                var permissionMapedData = await getMapedData(permissionService);

                var validRoles = from pmd in permissionMapedData
                                 where pmd.ResourceName.ToLower() == controller.ToLower() && pmd.PermissionTypes >= typeRequired
                                 where pmd.RoleName.ToLower() == roles.ToLower()
                                 select pmd.RoleId;

                if (!validRoles.Any())
                {
                    context.Response.StatusCode = 401;
                    return;
                }
                await next(context);
            }
            catch (System.Exception ex)
            {
                throw;
            }

        }

        public async Task<List<MappedData>> getMapedData(IPermissionService permissionService)
        {
            var cacheKey = "permissionMapedData";
            if (!memoryCache.TryGetValue(cacheKey, out List<MappedData> permissionMapedData))
            {
                List<Permission> permissionResult = new List<Permission>();

                var (IsSuccess, perResult) = await permissionService.GetPermissionAsync();

                if (IsSuccess)
                {
                    permissionResult = perResult.ToList();
                }

                permissionMapedData = (from p in permissionResult
                                       join k in ControllerMaps.GetControllerModuleMaps() on p.ProjectModule.ModuleName equals k.ModuleName
                                       select new MappedData
                                       {
                                           ModuleName = p.ProjectModule.ModuleName,
                                           ResourceName = k.ResourceName,
                                           PermissionTypes = p.PermissionType,
                                           RoleId = p.RoleId,
                                           RoleName = p.Role.RoleName.Replace(" ", string.Empty).ToLower()
                                       }).ToList();

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(3),
                    Priority = CacheItemPriority.High,

                };
                memoryCache.Set(cacheKey, permissionMapedData, cacheExpiryOptions);
            }

            return permissionMapedData;
        }

        public class MappedData
        {
            public string ModuleName { get; set; }
            public string ResourceName { get; set; }
            public PermissionTypes PermissionTypes { get; set; }
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }

    }
}
