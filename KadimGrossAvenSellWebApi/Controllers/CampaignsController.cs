using System;
using Business.Abstract;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AvenSellWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : Controller
    {
        private readonly ICampaignService _campaignService;

        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _campaignService.GetAllDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllForBasket")]
        public IActionResult GetAllForBasket(int basketId)
        {
            var result = _campaignService.GetAllForBasket(basketId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(CampaignAddDto campaignAddDto)
        {
            var result = _campaignService.Add(campaignAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}

