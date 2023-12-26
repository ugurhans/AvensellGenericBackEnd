using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Dto;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrate.EntityFramework
{
    public class EfAdminDal : EfEntityRepositoryBase<Admin, AvenSellContext>, IAdminDal
    {
        public List<OperationClaim> GetClaims(AdminDto adminDto)
        {
            using (var context = new AvenSellContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join uoc in context.UserOperationClaims
                                 on operationClaim.Id equals uoc.OperationClaimId
                             where uoc.UserId == adminDto.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();
            }
        }

        public AdminDto GetDto(string email)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from u in context.Admins
                             where u.Email == email
                             join b in context.Baskets on u.Id equals b.UserId into ps
                             from b in ps.DefaultIfEmpty()
                             select new AdminDto()
                             {
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Phone = u.PhoneNumber,
                                 Id = u.Id,
                                 PasswordHash = u.PasswordHash,
                                 PasswordSalt = u.PasswordSalt,
                                 Roles = (from occ in context.UserOperationClaims
                                          where u.Id == occ.UserId
                                          select new UserOperationClaim()
                                          {
                                              Id = occ.Id,
                                              OperationClaimId = occ.OperationClaimId,
                                              UserId = u.Id
                                          }).FirstOrDefault(),

                                 Status = u.Status,
                                 BasketId = b.Id
                             };

                var t = result.FirstOrDefault();
                return result.FirstOrDefault();
            }
        }

        public List<AdminProfileDtoIDto> GetAllAdmin()
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from u in context.Admins
                             select new AdminProfileDtoIDto()
                             {
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Id = u.Id,
                                 CreateDate = u.CreateDate,
                                 DateofBirth = u.DateofBirth,
                                 Gender = u.Gender,
                                 LastLogin = u.LastLogin,
                                 LastWrongLogin = u.LastWrongLogin,
                                 ModifiedDate = u.ModifiedDate,
                                 PhoneNumber = u.PhoneNumber,
                                 SocialMediaProfiles = u.SocialMediaProfiles,
                                 Status = u.Status
                             };
                return result.ToList();
            }
        }

      
    }
}
