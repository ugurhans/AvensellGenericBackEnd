using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KadimGrossAvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardSummariesController : Controller
    {
        private readonly IDashboardSummaryService _dashBoardSummaryService;

        public DashBoardSummariesController(IDashboardSummaryService dashBoardSummaryService)
        {
            _dashBoardSummaryService = dashBoardSummaryService;
        }

        [HttpGet("GetSumamry")]
        public IActionResult GetSumamry()
        {
            var result = _dashBoardSummaryService.GetSummary();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}

