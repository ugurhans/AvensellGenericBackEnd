using Business.Abstract;
using Core.Entities;
using Entity.Dto;
using Entity.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
                _couponService = couponService;
        }

        [HttpGet("GetAllForBasket")]
        public IActionResult GetAllForBasket(int basketId)
        {
            var result = _couponService.GetAllForBasket(basketId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(CouponAddDto CouponAddDto)
        {
            var result = _couponService.Add(CouponAddDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpGet("get/CouponType}/{id}")]
        //public IActionResult Get<T>(CouponTypes couponTypes, int id) where T : class, IEntity, new()
        //{
        //    var result = _couponService.Get<T>(couponTypes, id);
        //    if (result.Success)
        //    {
        //        return Ok(result.Data);
        //    }
        //    return BadRequest(result.Message);
        //}

        //[HttpGet("getcouponid/{id}")]
        //public IActionResult GetCouponId(int id)
        //{
        //    var result = _couponService.GetCouponId(id);
        //    if (result.Success)
        //    {
        //        return Ok(result.Data);
        //    }
        //    return BadRequest(result.Message);
        //}

        [HttpPut("update")]
        public IActionResult Update([FromBody] CouponUpdateDto CouponUpdateDto) 
        {
            var result = _couponService.Update(CouponUpdateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int couponıd, CouponTypes couponTypes)
        {
            var result = _couponService.Delete(couponıd, couponTypes);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _couponService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
