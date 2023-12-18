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
        Task<IDataResult<string>> ForgotPasswordSendLink(ForgotPasswordSendLinkDto email);
        IDataResult<bool> ForgotPasswordVerifyToken(ForgotPasswordChangesDto email);
        
    }
}

