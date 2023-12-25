using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Entity.Dto;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAdminService
    {
        AdminDto GetByMailDtoAdmin(string email);//
        List<OperationClaim> GetClaims(AdminDto admin);
        //List<OperationClaim> GetClaims(User user);//
        void Add(Admin admin);
        //IResult Update(User user);
        //IResult UpdateDto(UpdateUserDto user);
        //IResult UpdateMail(User user);
        //UserDto GetByMailDto(string email);
        //User GetByMail(string email);
        ////UserDto GetByMailPassword(string email);//
        //IResult Delete(int userId);
        //IDataResult<UserProfileDto>? GetUserProfile(int userId);
        //IDataResult<List<UserProfileDto>>? GetAllAdmins();
        //IDataResult<List<CarrierDto>> GetAllCarriers();
        //IDataResult<int> GetAllCount();
        //IDataResult<List<UserProfileDto>> GetAllUserProfile();
        //IDataResult<UserProfileDto> GetById(int id);//

     
    }
}
