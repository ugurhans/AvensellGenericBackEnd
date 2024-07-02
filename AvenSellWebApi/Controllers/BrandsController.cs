using Business.Abstract;
using Entity.Concrate;
using Microsoft.AspNetCore.Mvc;

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("SearchBrand")]
        public IActionResult SearchBrand(string brandName)
        {
            var result = _brandService.SearchBrand(brandName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        public IActionResult Add(Brand brand)
        {
            var result = _brandService.Add(brand);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}

