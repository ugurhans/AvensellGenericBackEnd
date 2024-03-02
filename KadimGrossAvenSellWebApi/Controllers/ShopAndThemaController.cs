//using Business.Abstract;
//using Core.Utilities.Results;
//using Entity.Concrate;
//using Entity.Dto;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Mvc;

//namespace KadimGrossAvenSellWebApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ShopAndThemaController : ControllerBase
//    {
//        private IShopService _shopService;
//        private IThemaService _themaService;
//        public ShopAndThemaController(IThemaService themaservice,IShopService shopService)
//        {
//            _shopService = shopService;
//            _themaService = themaservice;
//        }
//        [HttpGet("GetAll")]
//        public IActionResult GetAll()
//        {
//            var shopResult = _shopService.GetAll();
//            var themeResult = _themaService.GetAll();

//            if (shopResult.Success && themeResult.Success)
//            {
//                var combinedResult = new
//                {
//                    Shops = shopResult.Data,
//                    Themes = themeResult.Data
//                };

//                return Ok(combinedResult);
//            }

//            return BadRequest(new Result(false));
//        }

//        [HttpPost("Add")]
//        public IActionResult Add(ShopAndThemaDto shopAndThema)
//        {
//            var shopResult = _shopService.Add(shopAndThema.Shop);
//            var themeResult = _themaService.Add(shopAndThema.Colors);

//            if (shopResult.Success && themeResult.Success)
//            {
//                return Ok(new Result(true));
//            }
//            return BadRequest(new Result(false));
//        }


//        [HttpPatch("Update")]
//        public IActionResult Update(ShopAndThemaUpdateDto shopAndThema)
//        {
//            var shopResult = _shopService.Update(shopAndThema.Shop);
//            var themeResult = _themaService.Update(shopAndThema.Colors);


//            if (shopResult.Success && themeResult.Success)
//            {
//                return Ok(new Result(true));
//            }

//            return BadRequest(new Result(false));
//        }

//        [HttpDelete("Delete")]
//        public IActionResult Delete(int shopAndThemaId)
//        {
//            var shopResult = _shopService.Delete(shopAndThemaId);
//            var themeResult = _themaService.Delete(shopAndThemaId);

//            if (shopResult.Success && themeResult.Success)
//            {
//                return Ok(new Result(true));
//            }

//            return BadRequest(new Result(false));
//        }
//        [HttpGet("GetById")]
//        public IActionResult GetById(int ShopAndThemaId)
//        {
//            var shopResult = _shopService.GetById(ShopAndThemaId);
//            var themeResult = _themaService.GetById(ShopAndThemaId);

//            if (shopResult.Success && themeResult.Success)
//            {
//                var combinedResult = new
//                {
//                    Shop = shopResult.Data,
//                    Theme = themeResult.Data
//                };

//                return Ok(combinedResult);
//            }

//            return BadRequest(new Result(false));
//        }



//    }
//}
