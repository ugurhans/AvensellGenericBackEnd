using Business.Abstract;
using Core.Utilities.Results;
using Entity.Concrate;
using Entity.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketSettingController : ControllerBase
    {

        private IMarketSettingService _marketSettingService;

        public MarketSettingController(IMarketSettingService marketSettingService)
        {
            _marketSettingService = marketSettingService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var MarketSettingResult = _marketSettingService.GetAll();

            if (MarketSettingResult.Success)
            {
                var combinedResult = new
                {
                    MarketSetting = MarketSettingResult.Data,
                
                };

                return Ok(combinedResult);
            }

            return BadRequest(new Result(false));
        }

        [HttpPost("Add")]
        public IActionResult Add(MarketSetting marketSetting)
        {
            var MarketSettingResult = _marketSettingService.Add(marketSetting);

            if (MarketSettingResult.Success)
            {
                return Ok(new Result(true));
            }
            return BadRequest(new Result(false));
        }


        [HttpPost("Update")]
        public IActionResult Update(MarketSetting marketSetting)
        {
            var MarketSettingResult = _marketSettingService.Update(marketSetting);

            if (MarketSettingResult.Success)
            {
                return Ok(new Result(true));
            }

            return BadRequest(new Result(false));
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int MarketSettingId)
        {
            var MarketSettingResult = _marketSettingService.Delete(MarketSettingId);

            if (MarketSettingResult.Success)
            {
                return Ok(new Result(true));
            }

            return BadRequest(new Result(false));
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int MarketSettingId)
        {
            var MarketSettingResult = _marketSettingService.GetById(MarketSettingId);

            if (MarketSettingResult.Success)
            {
                var combinedResult = new
                {
                    marketsetting = MarketSettingResult.Data,
                };

                return Ok(combinedResult);
            }

            return BadRequest(new Result(false));
        }

    }
}
