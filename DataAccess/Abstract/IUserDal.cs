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
       
        List<CarrierDto> GetAllCarriers();
        List<UserProfileDto> GetAllUserProfile();
        List<OperationClaim> GetClaims(UserDto user);
        UserDto GetDto(string email);
        UserProfileDto GetProfileDto(int userId);
    }
}
