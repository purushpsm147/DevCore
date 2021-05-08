using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class AepLookupController : ControllerBase
    {
        private readonly IAepLookupService aepLookupService;

        public AepLookupController(IAepLookupService aepLookupService)
        {
            this.aepLookupService = aepLookupService;
        }
        [HttpPut]
        public async Task<IActionResult> PutAepLookup(SSTAepLookupGross sSTAep)
        {
            var result = await aepLookupService.PutAepAsync(sSTAep);
            if (result.IsSuccess)
            {
                return Ok(result.AepResults);
            }

            return NotFound(result.AepResults);
        }

        [HttpGet, Route("{aepGuid}")]
        public async Task<IActionResult> GetAepLookup(string aepGuid)
        {
            var result = await aepLookupService.GetAepAsync(aepGuid);
            if (result.IsSuccess)
            {
                return Ok(result.AepResults);
            }

            return new NoContentResult();
        }
    }
}

