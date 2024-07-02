using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AvenSellWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllDto")]
        public IActionResult GetAllDto()
        {
            var result = _categoryService.GetAllDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAllSimpleDtoDto")]
        public IActionResult GetAllSimpleDtoDto()
        {
            var result = _categoryService.GetAllSimpleDtoDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetallCategoryAndSubCategoriesDto")]
        public IActionResult GetallCategoryAndSubCategoriesDto()
        {
            var result = _categoryService.GetallCategoryAndSubCategoriesDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
