using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class OpportunitiesController : ControllerBase
    {
        private readonly IOpportunityService opportunityService;
        private readonly IMemoryCache memoryCache;
        private readonly string opportunityCacheKey = "opportunityList";
        public OpportunitiesController(IOpportunityService opportunityService, IMemoryCache memoryCache)
        {
            this.opportunityService = opportunityService;
            this.memoryCache = memoryCache;
        }

        #region Get Opportunities
        [HttpGet]
        [EnableQuery()]
        public async Task<IEnumerable<Project>> SearchOpportunityAsync()
        {
            if (!memoryCache.TryGetValue(opportunityCacheKey, out List<Project> opportunityList))
            {
                var result = await opportunityService.SearchOpportunityAsync();

                if (result.IsSuccess)
                {
                    opportunityList = result.OpportunityResults.ToList();

                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(15),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromMinutes(3)
                    };
                    memoryCache.Set(opportunityCacheKey, opportunityList, cacheExpiryOptions);
                }
            }

            return opportunityList;
        }

        /// <summary>
        /// TODO: Form authorize find the user... 
        /// </summary>
        /// <returns></returns>

        [HttpGet, Route("me"), EnableQuery()] //use OData Get  Opportunities
        public async Task<IEnumerable<Project>> GetMyOpportunities()
        {
            string user = User.Claims.FirstOrDefault(cl => cl.Type.Contains("preferred_username")).Value;
            var result = await opportunityService.GetMyOpportunityAsync(user);

            if (result.IsSuccess)
            {
                return result.OpportunityResults;
            }

            return new List<Project>();
        }

        [HttpGet, Route("{id:int}"), EnableQuery()]
        public async Task<Project> GetOpportunityById(int id)
        {
            var result = await opportunityService.GetOpportunityByIdAsync(id);
            var isCurrencyLocked = await opportunityService.GetCurrencyLockedDetails(id);

            if (result.IsSuccess)
            {
                var toRet = result.OpportunityResults.FirstOrDefault();
                toRet.IsCurrencyLocked = isCurrencyLocked;
                return toRet;
            }

            return null;

        }

        #endregion
        [HttpPut]
        public async Task<IActionResult> PutMyOpportunities(Project project)
        {
            //TODO: Violation of SRP
            bool ValidateIfOpportunityExists = await DoesOpportunityAlreadyExists(project.OpportunityId);
            if (ValidateIfOpportunityExists)
            {
                ModelState.AddModelError("OpportunityExists", "An Opportuity with same id already exists");
                return Conflict(ModelState);
            }

            if (project.HasDuplicateMilestones)
            {
                ModelState.AddModelError("DuplicateMileSones", "One or more MileStone present with same ID");
                return BadRequest(ModelState);
            }

            if (project.HasDuplicateRoles)
            {
                ModelState.AddModelError("Duplicate Roles", "One or more Role present with same ID");
                return BadRequest(ModelState);
            }
            var result = await opportunityService.PutProjectsAsync(project);

            if (result.IsSuccess)
            {
                memoryCache.Remove(opportunityCacheKey);
                return Ok(result.opportunityResults);
            }

            return NotFound(result.opportunityResults);
        }

        private async Task<bool> DoesOpportunityAlreadyExists(string opportunityId)
        {
            var result = await opportunityService.SearchOpportunityAsync();
            if (!result.IsSuccess)
                return false;
            return result.OpportunityResults.Any(op => op.OpportunityId == opportunityId);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PatchMyOpportunities(int id, [FromBody] Project project)
        {
            if (project.HasDuplicateMilestones)
            {
                ModelState.AddModelError("DuplicateMileSones", "One or more MileStone present with same ID");
                return BadRequest(ModelState);
            }

            if (project.HasDuplicateRoles)
            {
                ModelState.AddModelError("Duplicate Roles", "One or more Role present with same ID");
                return BadRequest(ModelState);
            }

            var result = await opportunityService.PatchProjectsAsync(id, project);

            if (result.IsSuccess)
            {
                memoryCache.Remove(opportunityCacheKey);
                return Ok(result.opportunityResults);
            }

            return NotFound(result.opportunityResults);
        }

        #region Get Proposals
        [HttpGet]
        [Route("{opportunityId}/Proposals"), EnableQuery()]
        public async Task<IEnumerable<Proposal>> GetProposalByOpportunity([FromServices] IProposalService proposalService, int opportunityId)
        {
            var result = await proposalService.GetProposalByOpportunityAsync(opportunityId);

            if (result.IsSuccess)
            {
                return result.ProposalResults;
            }

            return new List<Proposal>();
        }

        [HttpGet]
        [Route("{opportunityId}/Proposal/{proposalId}"), EnableQuery()]
        public async Task<IEnumerable<Proposal>> GetProposalById([FromServices] IProposalService proposalService, int opportunityId, int proposalId)
        {
            var result = await proposalService.GetProposalByOpportunityProposalIdAsync(opportunityId, proposalId);

            if (result.IsSuccess)
            {
                return result.ProposalResults;
            }

            return new List<Proposal>();
        }

        #endregion

    }
}
