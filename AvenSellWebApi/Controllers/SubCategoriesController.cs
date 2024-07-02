using Business.Abstract;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Entity.Concrate;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {

        private readonly ISubCategoryService _subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }


        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _subCategoryService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetAllDto")]
        public IActionResult GetAllDto()
        {
            var result = _subCategoryService.GetAllDto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetAllWithCategoryId")]
        public IActionResult GetAllWithCategoryId(int categoryId)
        {
            var result = _subCategoryService.GetAllWithCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("GetAllDtoWithCategoryId")]
        public IActionResult GetAllDtoWithCategoryId(int categoryId)
        {
            var result = _subCategoryService.GetAllDtoWithCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("SearchProductWithSubCategory")]
        public IActionResult SearchProductWithSubCategory(string searchString)
        {
            var result = _subCategoryService.SearchProductWithSubCategory(searchString);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
