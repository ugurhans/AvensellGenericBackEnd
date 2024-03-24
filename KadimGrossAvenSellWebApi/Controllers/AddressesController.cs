using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Entity.Concrete;
using Entity.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        IAddressService _addressesService;
        private readonly ICityService _cityService;
        private readonly IDistrictsService _districtsService;
        private readonly INeighborhoodService _neighborhoodService;

        public AddressesController(IAddressService addressesService, ICityService cityService, IDistrictsService districtsService, INeighborhoodService neighborhoodService)
        {
            _addressesService = addressesService;
            _cityService = cityService;
            _districtsService = districtsService;
            _neighborhoodService = neighborhoodService;
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
        
         [HttpGet("GetCity")]
        public IActionResult GetCity(int cityId)
        {
            var getCity = _cityService.GetCity(cityId);

            return Ok(getCity);
        }

        [HttpGet("GetAllCity")]
        public IActionResult GetAllCity()
        {
            var getAllCity = _cityService.GetAllCityList();
            return Ok(getAllCity);
        }
        [HttpPost("AddCity")]
        public IActionResult CityAdd(City city)
        {
            var send = _cityService.Add(city);
            return Ok(send);
        }
        [HttpGet("GetDistrict")]
        public IActionResult GetDistrict(int districtId)
        {
            var getDistrict = _districtsService.GetDistrict(districtId);
            return Ok(getDistrict);
        }
        [HttpGet("GetAllDistrict")]
        public IActionResult GetAllDistrict(int cityId)
        {
            var getAllDistrict = _districtsService.GetAllDistricts(cityId);
            return Ok(getAllDistrict);
        }


        [HttpPost("GetAllDistrictsWithCities")]
        public IActionResult GetAllDistrictsWithCities(List<int> cities)
        {
            var getAllDistrict = _districtsService.GetAllDistrictsWithCities(cities);
            return Ok(getAllDistrict);
        }
        [HttpPost("AddDistrict")]
        public IActionResult AddDistrict(District district)
        {
            var send = _districtsService.Add(district);
            return Ok(send);
        }
        [HttpGet("GetNeighborhood")]
        public IActionResult GetNeighborhood(int neighborhoodId)
        {
            var getNeighborhood = _neighborhoodService.Get(neighborhoodId);
            return Ok(getNeighborhood);
        }
        [HttpGet("GetAllNeighborhood")]
        public IActionResult GetAllNeighborhood(int districtId)
        {
            var getAllNeighborhood = _neighborhoodService.GetAllByDistrict(districtId);
            return Ok(getAllNeighborhood);
        }
        [HttpPost("AddNeighborhood")]
        public IActionResult AddNeighborhood(Neighborhood neighborhood)
        {
            var send = _neighborhoodService.Add(neighborhood);
            return Ok(send);
        }
        
    }
}
