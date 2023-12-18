using System;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entity.Dto;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserDto> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<UserDto> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<UserDto> CreateAccessToken(UserDto user);
        Task<IResult> SendPasswordResetMailAsync(string userMail);
        IResult ChangePassword(string userMail, string code, string newPassword);
    }
}

