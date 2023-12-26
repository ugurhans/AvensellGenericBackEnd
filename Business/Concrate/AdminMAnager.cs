using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class AdminMAnager : IAdminService
    {
        private IAdminDal _adminDal;

        public AdminMAnager(IAdminDal adminDal)
        {
            _adminDal = adminDal;


        }

        public void Add(Admin admin)
        {
            _adminDal.Add(admin);
        }

        //public IDataResult<AdminDto> LoginAdmin(AdminForLoginDto adminForLoginDto)
        //{
        //    var userToCheck = _userService.GetByMailDtoAdmin(adminForLoginDto.Email);
        //    if (userToCheck == null)
        //    {
        //        return new ErrorDataResult<AdminDto>("E-posta'ya ait bir kayıt bulunamadı.");
        //    }
        //    if (!HashingHelper.VerifyPasswordHash(adminForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        //    {
        //        return new ErrorDataResult<AdminDto>(Messages.Error);
        //    }
        //    return new SuccessDataResult<AdminDto>(userToCheck, "Giriş Başarıyla Sağlandı.");
        //}
        public AdminDto GetByMailDtoAdmin(string email)
        {
            return _adminDal.GetDto(email);
        }

        public List<OperationClaim> GetClaims(AdminDto admin)
        {
            return _adminDal.GetClaims(admin);
        }
    }
}
