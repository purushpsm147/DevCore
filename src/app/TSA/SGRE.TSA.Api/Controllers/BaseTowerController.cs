using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Models.Enums;
using SGRE.TSA.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class BaseTowerController : ControllerBase
    {
        private readonly IBaseTowerService _baseTowerService;

        public BaseTowerController(IBaseTowerService baseTowerService)
        {
            _baseTowerService = baseTowerService;
        }

        [HttpPut]
        public async Task<IActionResult> PutBaseTower(BaseTower baseTower)
        {
            if (baseTower.ScenarioType == ScenarioTypes.SST)
            {
                ModelState.AddModelError("Error", "you can not add sst scenario in base tower");
                return BadRequest(ModelState);
            }

            var result = await _baseTowerService.PutBaseTowerAsync(baseTower);

            if (!result.IsSuccess)
            {
                return BadRequest(result.baseTowerResult);
            }
            return Ok(result.baseTowerResult);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IEnumerable<BaseTower>> GetBaseTower(int id)
        {
            var result = await _baseTowerService.GetBaseTowerAsync(id);
            if (result.IsSuccess)
            {
                return result.baseTowerResults;
            }
            return new List<BaseTower>();
        }

        [HttpPatch]
        [Route("{Id}")]
        public async Task<IActionResult> PatchBaseTower(int Id, [FromBody] BaseTower baseTower)
        {
            if (baseTower.Id == 0)
            {
                ModelState.AddModelError("Id Required", "Id is Required for Patch STP/ETP Operation");
                return BadRequest(ModelState);
            }
            var result = await _baseTowerService.PatchBaseTowerAsync(Id, baseTower);

            if (result.IsSuccess)
            {
                return Ok(result.baseTowerResult);
            }

            return NotFound(result.baseTowerResult);
        }

    }
}
