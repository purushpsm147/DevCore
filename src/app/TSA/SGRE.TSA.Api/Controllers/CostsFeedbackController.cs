using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class CostsFeedbackController : ControllerBase
    {
        private readonly ICostsFeedbackService costsFeedbackService;
        public CostsFeedbackController(ICostsFeedbackService costsFeedbackService)
        {
            this.costsFeedbackService = costsFeedbackService;
        }

        [HttpPut]
        public async Task<IActionResult> PutCostFeedback(CostFeedback costFeeback)
        {
            var result = await costsFeedbackService.PutCostFeebackAsync(costFeeback);

            if (!result.IsSuccess)
            {
                return NotFound(result.costFeebackResults);
            }
            return Ok(result.costFeebackResults);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PatchCostFeedback(int id, [FromBody] CostFeedback costFeeback)
        {
            if (id == 0 || costFeeback == null)
            {
                ModelState.AddModelError("Invalid data", "data invalida to do patch operation");
                return BadRequest(ModelState);
            }
            var result = await costsFeedbackService.PatchCostFeebackAsync(id, costFeeback);

            if (result.IsSuccess)
            {
                return Ok(result.costFeebackResults);
            }

            return NotFound(result.costFeebackResults);

        }

        [HttpGet]
        [Route("{scenarioId:int}"), EnableQuery()]
        public async Task<IActionResult> GetCostFeedback(int scenarioId)
        {
            if (scenarioId == 0)
            {
                ModelState.AddModelError("Invalid Id", "Please provide scenario Id in URL");
                return BadRequest(ModelState);
            }
            var result = await costsFeedbackService.GetCostFeeback(scenarioId);

            if (result.IsSuccess)
            {
                return Ok(result.costFeebackResults);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("CostOverView/{scenarioId:int}"), EnableQuery()]
        public async Task<IActionResult> GetCostOverView(int scenarioId)
        {
            if (scenarioId == 0)
            {
                ModelState.AddModelError("Invalid Id", "Please provide scenario Id in URL");
                return BadRequest(ModelState);
            }
            var result = await costsFeedbackService.GetCostOverView(scenarioId);

            if (result.IsSuccess)
            {
                return Ok(result.costOverViewResults);
            }

            return NotFound();
        }
    }
}
