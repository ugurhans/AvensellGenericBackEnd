using Business.Abstract;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using Core.Entities.Concrete;
using Entity.Concrete;
using Entity.Dto;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IBasketService _basketService;
        private IUserService _userService;

        public AuthController(IAuthService authService, IBasketService basketService, IUserService userService)
        {
            _authService = authService;
            _basketService = basketService;
            _userService = userService;
        }


        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                result.Data.BasketId = userToLogin.Data.BasketId;
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("SendPasswordResetMail")]
        public async Task<ActionResult> SendPasswordResetMail(string userMail)
        {
            var mailInfo = await _authService.SendPasswordResetMailAsync(userMail);
            if (!mailInfo.Success)
            {
                return BadRequest(mailInfo);
            }
            return Ok(mailInfo);
        }


        [HttpPost("ChangePassword")]
        public ActionResult ChangePassword(string userMail, string code, string newPassword)
        {
            var mailInfo = _authService.ChangePassword(userMail, code, newPassword);
            if (!mailInfo.Success)
            {
                return BadRequest(mailInfo);
            }
            return Ok(mailInfo);
        }



        //[HttpPost("Delete")]
        //public ActionResult Delete(int userId)
        //{
        //    var result = _authService.Delete(userId);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(result);
        //}


        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var basket = new Basket()
            {
                UserId = registerResult.Data.Id,
                CreatedDate = DateTime.Now,
            };
            var basketResult = _basketService.Add(basket);
            var tokenResult = _authService.CreateAccessToken(registerResult.Data);

            if (registerResult.Success && tokenResult.Success && basketResult.Success)
            {
                tokenResult.Data.BasketId = basket.Id;
                return Ok(tokenResult);
            }

            return BadRequest(tokenResult);
        }



        //[HttpPost("resetpassword")]
        //public ActionResult ForgotPassword(ResertPasswordDto resertPasswordDto)
        //{
        //    var result = _authService.ForgotPassword(resertPasswordDto);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(result);
        //}

        [HttpPost("updateUser")]
        public ActionResult UpdateUser(UpdateUserDto user)
        {
            var result = _userService.UpdateDto(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("updateUserMail")]
        public ActionResult UpdateUserMail(User user)
        {
            var result = _userService.UpdateMail(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetUserProfile")]
        public ActionResult GetUserProfile(int userId)
        {
            var result = _userService.GetUserProfile(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("GetAllCarriers")]
        public ActionResult GetAllCarriers()
        {
            var result = _userService.GetAllCarriers();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAllUserProfile")]
        public ActionResult GetAllUserProfile()
        {
            var result = _userService.GetAllUserProfile();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("GetAllAdmins")]
        public ActionResult GetAllAdmins()
        {
            var result = _userService.GetAllAdmins();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}