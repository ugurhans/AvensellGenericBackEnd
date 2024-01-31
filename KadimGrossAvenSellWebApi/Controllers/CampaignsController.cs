using System;
using Business.Abstract;
using Core.Entities;
using Entity.Dto;
using Entity.Enum;
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

        //[HttpGet("getall")]
        //public IActionResult GetAll()
        //{
        //    var result = _campaignService.GetAllDto();
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

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

        [HttpGet("get/{campaignType}/{id}")]
        public IActionResult Get<T>(CampaignTypes campaignType, int id) where T : class, IEntity, new()
        {
            var result = _campaignService.Get<T>(campaignType, id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        //[HttpGet("getcampaignid/{id}")]
        //public IActionResult GetCampaignID(int id)
        //{
        //    var result = _campaignService.GetCampaignID(id);
        //    if (result.Success)
        //    {
        //        return Ok(result.Data);
        //    }
        //    return BadRequest(result.Message);
        //}

        [HttpPut("update")]
        public IActionResult Update([FromBody] CampaignUpdateDto campaignUpdateDto)
        {
            var result = _campaignService.Update(campaignUpdateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete/{campaignType}/{campaignId}")]
        public IActionResult Delete(int campaignId, CampaignTypes campaignType)
        {
            var result = _campaignService.Delete(campaignId, campaignType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _campaignService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}

