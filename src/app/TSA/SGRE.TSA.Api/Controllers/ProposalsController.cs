using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class ProposalsController : ControllerBase
    {
        private readonly IProposalService proposalService;

        public ProposalsController(IProposalService proposalService)
        {
            this.proposalService = proposalService;
        }

        [HttpPut]
        public async Task<IActionResult> PutMyProposal(Proposal proposal)
        {
            bool ValidateIfProposalExists = await DoesProposalAlreadyExists(proposal);
            if (ValidateIfProposalExists)
            {
                ModelState.AddModelError("ProposalExists", $"A Proposal with same SalesForce Id already exists in Opportunity {proposal.ProjectId}");
                return BadRequest(ModelState);
            }

            if (await CheckMainProposal(proposal) && proposal.IsMain)
            {
                ModelState.AddModelError("IsMain", "InValid Form, Main Proposal already exists");
                return Conflict(ModelState);
            }
            if (proposal.ProjectId == 0)
            {
                ModelState.AddModelError("ProjectIdRequired", "ProjectId is Required for Put Proposal Operation");
                return BadRequest(ModelState);
            }
            var result = await proposalService.PutProposalAsync(proposal);

            if (!result.IsSuccess)
            {
                return NotFound(result.proposalResults);
            }
            return Ok(result.proposalResults);
        }

        /*
         * TODO: Multiple Get Proposal Calls for validations is expensive. Optimization required.
         * Can't cache data as the database is updated after every Put/Patch operation.
         */
        private async Task<bool> DoesProposalAlreadyExists(Proposal proposal)
        {
            var result = await proposalService.GetProposalByOpportunityAsync(proposal.ProjectId);
            if (!result.IsSuccess)
                return false;
            return result.ProposalResults.Any(pro => string.Equals(pro.ProposalId, proposal.ProposalId));
        }

        private async Task<bool> CheckMainProposal(Proposal proposal)
        {
            var result = await proposalService.GetProposalByOpportunityAsync(proposal.ProjectId);
            if (!result.IsSuccess)
                return false;
            return result.ProposalResults.Any(p => p.IsMain);
        }
        private async Task<bool> CheckMainProposal(Proposal proposal, int id)
        {
            var result = await proposalService.GetProposalByOpportunityAsync(proposal.ProjectId);
            if (!result.IsSuccess)
                return false;
            var mainProposal = result.ProposalResults.FirstOrDefault(p => p.IsMain);
            if (mainProposal != null && mainProposal.Id == id)
            {
                return false;
            }

            if (mainProposal != null && mainProposal.Id != id)
            {
                return true;
            }
            return false;
        }

        [HttpPatch]
        [Route("{Id}")]
        public async Task<IActionResult> PatchMyProposal(int Id, [FromBody] Proposal proposal)
        {
            if (proposal.IsMain && await CheckMainProposal(proposal, Id))
            {
                ModelState.AddModelError("IsMain", "InValid Form, Main Proposal already exists");
                return BadRequest(ModelState);
            }

            if (proposal.ProjectId == 0)
            {
                ModelState.AddModelError("ProjectIdRequired", "ProjectId is Required for Patch Proposal Operation");
                return BadRequest(ModelState);
            }
            var result = await proposalService.PatchProposalAsync(Id, proposal);

            if (result.IsSuccess)
            {
                return Ok(result.proposalResults);
            }

            return NotFound(result.proposalResults);

        }

        [HttpGet, Route("{id:int}"), EnableQuery()]
        public async Task<Proposal> GetProposalById(int id)
        {
            var result = await proposalService.GetProposalByIdAsync(id);

            if (result.IsSuccess)
            {
                return result.ProposalResults.FirstOrDefault();
            }

            return null;

        }

    }
}
