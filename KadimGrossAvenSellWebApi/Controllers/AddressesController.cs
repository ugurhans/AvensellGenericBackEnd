using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        IAddressService _addressesService;

        public AddressesController(IAddressService addressesService)
        {
            _addressesService = addressesService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll(int userId) //bir kullanicinin tum adresleri
        {
            var result = _addressesService.GetAll(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getSelectedAddress")]
        public IActionResult GetSelectedAdress(int addressId)
        {
            var result = _addressesService.GetSelectedAddress(addressId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Address adress)
        {
            var result = _addressesService.Add(adress);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(Address adress)
        {
            var result = _addressesService.Update(adress);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            var result = _addressesService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("SetIsActive")]
        public IActionResult SetIsActive(int id)
        {
            var result = _addressesService.SetIsActive(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
