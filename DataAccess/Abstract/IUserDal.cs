using Core.DataAccess;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entity.DTOs;
using Entity.Dto;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<UserProfileDto> GetAllAdmin();
        List<CarrierDto> GetAllCarriers();
        List<UserProfileDto> GetAllUserProfile();
        List<OperationClaim> GetClaims(UserDto user);
       // List<OperationClaim> GetClaims(User user);//
        UserDto GetDto(string email);
        //User GetUserById(int id);
        UserProfileDto GetProfileDto(int userId);
    }
}
