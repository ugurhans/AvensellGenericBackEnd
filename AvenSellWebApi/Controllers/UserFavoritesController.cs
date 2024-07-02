using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavoritesController : ControllerBase
    {
        private IUserFavoriteService _userFavoriteService;

        public UserFavoritesController(IUserFavoriteService userFavoriteService)
        {
            _userFavoriteService = userFavoriteService;
        }

        [HttpGet("GetDtoByUserId")]
        public IActionResult GetDtoByUserId(int userId)
        {
            var result = _userFavoriteService.GetByUserIdAllProducts(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetSimpleDtoByUserId")]
        public IActionResult GetSimpleDtoByUserId(int userId)
        {
            var result = _userFavoriteService.GetSimpleDtoByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // [HttpGet("GetAllDtoByUserId")]
        // public IActionResult GetAllDtoByUserId(int userId)
        // {
        //     var result = _userFavoriteService.GetByUserId(userId);
        //     if (result.Success)
        //     {
        //         return Ok(result);
        //     }
        //
        //     return BadRequest(result);
        // }

        [HttpPost("Add")]
        public IActionResult Add(UserFavorite favorite)
        {
            var result = _userFavoriteService.Add(favorite);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("Delete")]
        public IActionResult Delete(int userId, int productId)
        {
            var result = _userFavoriteService.Delete(userId, productId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
