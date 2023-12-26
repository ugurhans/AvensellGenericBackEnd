using System;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entity.Dto;
using Entity.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(UserDto user);
        //List<OperationClaim> GetClaims(User user);//
        void Add(User user);
        IResult Update(User user);
        IResult UpdateDto(UpdateUserDto user);
        IResult UpdateMail(User user);
        UserDto GetByMailDto(string email);
        User GetByMail(string email);
        //UserDto GetByMailPassword(string email);//
        IResult Delete(int userId);
        IDataResult<UserProfileDto>? GetUserProfile(int userId);
        IDataResult<List<CarrierDto>> GetAllCarriers();
        IDataResult<int> GetAllCount();
        IDataResult<List<UserProfileDto>> GetAllUserProfile();
        IDataResult<UserProfileDto> GetById(int id);//

        
       // List<OperationClaim> GetClaims(User user);//
    }
}

