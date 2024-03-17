using System;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Entity.Dto;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserDto> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<AdminDto> RegisterAdmin(AdminForRegisterDto adminForRegisterDto, string password);
        IDataResult<UserDto> Login(UserForLoginDto userForLoginDto);
        IDataResult<AdminDto> LoginAdmin(AdminForLoginDto adminForLoginDto);
        IResult UserExists(string email);
        IDataResult<UserDto> CreateAccessToken(UserDto user);
        IDataResult<AdminDto> CreateAccessTokenAdmin(AdminDto admin);
        Task<IResult> SendPasswordResetMailAsync(string userMail);
        IResult ChangePassword(string userMail, string code, string newPassword);
        
        Task<IResult> SendOtpMail(string userMail);

    }
}

