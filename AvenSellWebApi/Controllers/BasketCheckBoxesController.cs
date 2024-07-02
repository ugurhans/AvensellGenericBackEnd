using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entity.Dto;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketCheckBoxesController : Controller
    {
        private readonly IBasketBoxesService _basketBoxesService;

        public BasketCheckBoxesController(IBasketBoxesService basketBoxesService)
        {
            _basketBoxesService = basketBoxesService;
        }

        [HttpGet("GetBoxes")]
        public IActionResult GetDetailByUserId()
        {
            var result = _basketBoxesService.GetBoxes();

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("UpdateBoxes")]
        public IActionResult UpdateBoxes(BasketCheckBoxTypeDto basketCheckBoxes)
        {
            var result = _basketBoxesService.UpdateBoxes(basketCheckBoxes);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

