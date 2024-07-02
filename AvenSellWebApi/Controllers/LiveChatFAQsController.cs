using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entity.Concrate;
using Entity.Enum;
using Entity.Request;
using MailKit;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiveChatFAQsController : Controller
    {
        private readonly ILiveChatFAQsService _liveChatFAQsService;

        public LiveChatFAQsController(ILiveChatFAQsService liveChatFAQsService)
        {
            _liveChatFAQsService = liveChatFAQsService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _liveChatFAQsService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(LiveChatFAQs liveChatFAQs)
        {
            var result = _liveChatFAQsService.Update(liveChatFAQs);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(LiveChatFAQs liveChatFAQs)
        {
            var result = _liveChatFAQsService.Add(liveChatFAQs);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("ToggleActive")]
        public IActionResult ToggleActive(int id)
        {
            var result = _liveChatFAQsService.SetActive(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            var result = _liveChatFAQsService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}

