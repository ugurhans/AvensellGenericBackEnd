using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Entity.Concrete;

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressesService;

        public AddressesController(IAddressService addressesService)
        {
            _addressesService = addressesService;
        }

        [HttpGet("GetAllByUserId")]
        public IActionResult GetAllByUserId(int userId)
        {
            var result = _addressesService.GetAllByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Address address)
        {
            var result = _addressesService.Add(address);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Update")]
        public IActionResult Update(Address address)
        {
            var result = _addressesService.Update(address);
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

        [HttpGet("GetAllCity")]
        public IActionResult GetAllCity()
        {
            var result = _addressesService.GetAllCity();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllDistrictWithCityId")]
        public IActionResult GetAllDistrictWithCityId(int cityId)
        {
            var result = _addressesService.GetAllDistrictWithCityId(cityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllMuhitWithDistrictId")]
        public IActionResult GetAllMuhitWithDistrictId(int districtId)
        {
            var result = _addressesService.GetAllMuhitWithDistrictId(districtId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllNeighbourhoodWithMuhitId")]
        public IActionResult GetAllNeighbourhoodWithMuhitId(int muhitId)
        {
            var result = _addressesService.GetAllNeighbourhoodWithMuhitId(muhitId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
