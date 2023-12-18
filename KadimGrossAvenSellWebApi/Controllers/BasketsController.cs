using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;
using Entity.Dto;
using Entity.Enum;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Request;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IBasketItemService _basketItemService;

        public BasketsController(IBasketService basketService, IBasketItemService basketItemService)
        {
            _basketService = basketService;
            _basketItemService = basketItemService;
        }

        [HttpGet("GetDetailByUserId")]
        public IActionResult GetDetailByUserId(int userId)
        {
            var result = _basketService.GetDetailByUserId(userId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetSimpleByUserId")]
        public IActionResult GetSimpleByUserId(int userId)
        {
            var result = _basketService.GetSimpleByUserId(userId);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("ApplyCampaign")]
        public IActionResult ApplyCampaign(int basketId, int campaignId, CampaignTypes campaignType)
        {
            IDataResult<BasketDetailDto> result = null;
            switch (campaignType)
            {
                case CampaignTypes.GiftCampaign:
                    result = _basketService.ApplyGiftCampaign(campaignId, basketId);
                    break;
                case CampaignTypes.ProductGroupCampaign:
                    result = _basketService.ApplyProductGroupCampaign(campaignId, basketId);
                    break;
                case CampaignTypes.SecondDiscountCampaign:
                    result = _basketService.ApplySecondDiscountCampaign(campaignId, basketId);
                    break;
                default:
                    result = new ErrorDataResult<BasketDetailDto>("Kampanya Uygulanırken Sorun Yaşandı.");
                    break;
            }
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        [HttpPost("AddItem")]
        public IActionResult AddItem(BasketAddItemDto basketItem)
        {
            var result = _basketItemService.Add(basketItem);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("DeleteItem")]
        public IActionResult DeleteItem(int basketId, int productId)
        {
            var result = _basketItemService.Delete(productId, basketId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("ClearbasketCampaign")]
        public IActionResult ClearbasketCampaign(int basketId)
        {
            var result = _basketService.ClearbasketCampaign(basketId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("DeleteAllBasket")]
        public IActionResult DeleteAllBasket(int basketId)
        {
            var result = _basketItemService.DeleteAllBasket(basketId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("DeleteAllItem")]
        public IActionResult DeleteAllItem(int productId, int basketId)
        {
            var result = _basketItemService.DeleteAllItem(productId, basketId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getBadgeCount")]
        public IActionResult GetBadgeCount(int userId)
        {
            var result = _basketService.GetBadgeCount(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result);

        }
    }
}
