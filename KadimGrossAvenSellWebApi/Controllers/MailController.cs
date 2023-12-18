using Business.Abstract;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("sendLostMail")]
        public async Task<IActionResult> SendLostMail(MailRequest request)
        {
            var result = await mailService.SendLostEmailAsync(request);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("sendResetMail")]
        public async Task<IActionResult> SendResetMail(MailRequest request)
        {
            var result = await mailService.SendResetEmailAsync(request);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}

