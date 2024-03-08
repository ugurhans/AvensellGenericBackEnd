using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Entity.Concrete;
using System;
using Entity.Enum;
using Entity.Dto;
using Entity.Concrate.paytr;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IFileStorageService _fileStorageService;

        public OrdersController(IOrderService orderService, IFileStorageService fileStorageService)
        {
            _orderService = orderService;
            _fileStorageService = fileStorageService;
        }

       

        [HttpGet("GetOrderBasicByUserId/{userId}")]
        public IActionResult GetOrderBasicByUserId(int userId)
        {
            var result = _orderService.GetOrderBasicByUserId(userId);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }



        [HttpGet("GetById")]
        public IActionResult GetById(int orderId)
        {
            var result = _orderService.GetById(orderId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }



        [HttpGet("GetAllWithParts")]
        public IActionResult GetAllWithParts()
        {
            var result = _orderService.GetAllWithParts();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetLastOrdersSimple")]
        public IActionResult GetLastOrdersSimple()
        {
            var result = _orderService.GetLastOrdersSimple();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("CancelOrder")]
        public IActionResult CancelOrder(int orderId)
        {
            var result = _orderService.CancelOrder(orderId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("UpdateWithState")]
        public IActionResult UpdateWithState(int orderId, OrderStates state)
        {
            var result = _orderService.UpdateWithState(orderId: orderId, state: state);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("RepeatOrder")]
        public IActionResult RepeatOrder(int orderId)
        {
            var result = _orderService.RepeatOrder(orderId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("CompleteOrderForPaytr")] //for paytr
        [Consumes("application/x-www-form-urlencoded", "application/json")]
        public async Task<IActionResult> CompleteOrderForPaytr()
        {
            //
            // var formData = HttpContext.Request.Form;
            // var paytrWebHookDto = new PaytrWebHookDto();
            // paytrWebHookDto.merchant_oid = formData["merchant_oid"];
            // paytrWebHookDto.status = formData["status"];
            // _orderService.OrderComplate(paytrWebHookDto);
            return Ok("OK");
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> Add(OrderCreateRequestDto order)
        {
            var result = await _orderService.AddPayTr(order);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        // [HttpGet("GetAllByUserId")]
        // public IActionResult GetAllByUserId(int userId, OrderStates state)
        // {
        //     var result = _orderService.GetAllDto(userId, state);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //
        //     return BadRequest(result);
        
        
        // }
        // [HttpGet("getallByState")]
        // public IActionResult GetAllByState(OrderStates state, DateTime? dateStart, DateTime? dateEnd)
        // {
        //     var result = _orderService.GetAllByState(state, dateStart, dateEnd);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //
        //     return BadRequest(result);
        // }

        // [HttpGet("GetBadgeWithState")]
        // public IActionResult GetBadgeWithState(OrderStates stateId)
        // {
        //     var result = _orderService.GetBadgeWithState(stateId);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //
        //     return BadRequest(result);
        // }
        //
        // [HttpPost("CompleteOrder")]
        // public IActionResult CompleteOrder(int orderId)
        // {
        //     var result = _orderService.CompleteOrder(orderId);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //
        //     return BadRequest(result);
        // }


       
        // [HttpDelete("Delete")]
        // public IActionResult Delete(int orderId)
        // {
        //     var result = _orderService.Delete(orderId);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //
        //     return BadRequest(result);
        // }

       

        // [HttpPost("Update")]
        // public IActionResult Update(OrderUpdateDto orderUpdate)
        // {
        //     var result = _orderService.Update(orderUpdate);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //     return BadRequest(result);
        // }


        //
        // [HttpGet("GetOrdersWithCount")]
        // public IActionResult GetOrdersWithCount(int orderCount)
        // {
        //     var result = _orderService.GetOrdersWithCount(orderCount);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //
        //     return BadRequest(result);
        // }
    }
}