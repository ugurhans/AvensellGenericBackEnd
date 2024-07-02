using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entity.Concrate;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopStuffController : Controller
    {
        private IShopStuffService _shopStuffService;

        public ShopStuffController(IShopStuffService shopStuffService)
        {
            _shopStuffService = shopStuffService;
        }

        [HttpGet("GetAllUserPermissionsWithUserId")]
        public IActionResult GetAllUserPermissionsWithUserId(int userId)
        {
            var result = _shopStuffService.GetAllUserPermissionsWithUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateUserPermissions")]
        public IActionResult UpdateUserPermissions(UserPermission userPermission)
        {
            var result = _shopStuffService.UpdateUserPermission(userPermission);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetAllFAQs")]
        public IActionResult GetAllFAQs()
        {
            var result = _shopStuffService.GetAllFAQs();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetFAQsWithId")]
        public IActionResult GetFAQsWithId(int id)
        {
            var result = _shopStuffService.GetFAQsWithId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        [HttpPost("UpdateFAQs")]
        public IActionResult UpdateFAQs(FAQs fAQs)
        {
            var result = _shopStuffService.UpdateFaqs(fAQs);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddFAQs")]
        public IActionResult AddFAQs(FAQs fAQs)
        {
            var result = _shopStuffService.AddFAQs(fAQs);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("DeleteFAQsWithId")]
        public IActionResult DeleteFAQsWithId(int id)
        {
            var result = _shopStuffService.DeleteFaqs(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }




        [HttpGet("GetMarketVariables")]
        public IActionResult GetMarketVariables()
        {
            var result = _shopStuffService.GetMarketVariables();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdateMarketVariables")]
        public IActionResult UpdateMarketVariables(MarketVariable marketVariables)
        {
            var result = _shopStuffService.UpdateMarketVariables(marketVariables);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("AddMarketVariables")]
        public IActionResult AddMarketVariables(MarketVariable marketVariables)
        {
            var result = _shopStuffService.AddMarketVariables(marketVariables);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

