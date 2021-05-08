using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SGRE.TSA.Models;
using SGRE.TSA.Services.Services;
using System.Threading.Tasks;

namespace SGRE.TSA.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        [HttpPut]
        [Route("sendMail")]
        public async Task<IActionResult> SendMail(MailBody mailBody, [FromServices] IConfiguration config)
        {
            mailBody.UILInk = config.GetSection("UI").Value;
            var result = await _supportService.SendMail(mailBody);
            if (result.IsSuccess)
            {
                return StatusCode(202);
            }

            return NotFound();
        }

    }
}
