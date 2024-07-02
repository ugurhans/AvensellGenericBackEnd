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
    public class ChartsController : Controller
    {
        private readonly IChartsService _chartService;

        public ChartsController(IChartsService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet("GetCategoryForOrderChart")]
        public IActionResult GetCategoryForOrderChart()
        {
            var result = _chartService.GetCategoryForOrderChart();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetProductForClick")]
        public IActionResult GetProductForClick()
        {
            var result = _chartService.GetProductForClick();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("GetProductForWaitingInBasket")]
        public IActionResult GetProductForWaitingInBasket()
        {
            var result = _chartService.GetProductForWaitingInBasket();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("GetProductForLowSelling")]
        public IActionResult GetProductForLowSelling()
        {
            var result = _chartService.GetProductForLowSelling();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("GetCostForMarket")]
        public IActionResult GetCostForMarket()
        {
            var originalDataResult = _chartService.GetCostForMarket();
            if (originalDataResult.Success)
            {
                return Ok(originalDataResult);
            }
            else
            {
                return BadRequest(originalDataResult.Message);
            }
        }
    }
}

