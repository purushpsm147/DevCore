using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class SstTowerController : ControllerBase
    {
        private readonly ISstTowerService _iSstTowerService;

        public SstTowerController(ISstTowerService iSSTInputService)
        {
            _iSstTowerService = iSSTInputService;
        }

        [HttpGet]
        [Route("{sstId:int}"), EnableQuery()]
        public async Task<IEnumerable<SstTower>> GetSstTowerById(int sstId)
        {
            var result = await _iSstTowerService.GetSstTowerIdAsync(sstId);
            if (result.IsSuccess)
            {
                return result.sstResults;
            }
            return new List<SstTower>();
        }

        [HttpPut]
        public async Task<IActionResult> PutSstTower(SstTower sstInput)
        {
            var result = await _iSstTowerService.PutSstTowerAsync(sstInput);

            if (!result.IsSuccess)
            {
                return NotFound(result.sstResults);
            }
            return Ok(result.sstResults);
        }

        [HttpPatch]
        [Route("{Id}")]
        public async Task<IActionResult> PatchSstTower(int Id, [FromBody] SstTower sstInput)
        {
            if (sstInput.Id == 0)
            {
                ModelState.AddModelError("Id Required", "Id is Required for Patch sstInput Operation");
                return BadRequest(ModelState);
            }
            var result = await _iSstTowerService.PatchSstTowerAsync(Id, sstInput);

            if (result.IsSuccess)
            {
                return Ok(result.sstResults);
            }

            return NotFound(result.sstResults);

        }

        [HttpGet]
        [Route("GetInitialTower"), EnableQuery()]
        public async Task<IEnumerable<InitialTower>> GetInitialTower(int wtgCatalogueId, int WtgCatalogueModelId, decimal ProposedHubHeight)
        {
            var result = await _iSstTowerService.GetIntialTowerAsync(wtgCatalogueId, WtgCatalogueModelId, ProposedHubHeight);
            if (result.IsSuccess)
            {
                return result.sstResults;
            }
            return new List<InitialTower>();
        }
    }
}
