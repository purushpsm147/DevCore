using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGRE.TSA.Services.Services;
using Microsoft.AspNetCore.Authorization;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class RolesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostMyRoles([FromBody] IEnumerable<ProjectRoles> projectRoles, [FromServices] IAssignRolesService assignRoles)
        {
            var result = await assignRoles.PostRolesAsync(projectRoles);
            if (result.IsSuccess)
            {
                return Ok(result.rolesAdded);
            }
            return NotFound();
        }

    }
}
