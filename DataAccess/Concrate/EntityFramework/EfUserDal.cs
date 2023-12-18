using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entity.DTOs;
using Entity.Concrate;
using Entity.Dto;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, AvenSellContext>, IUserDal
    {
        public List<UserProfileDto> GetAllAdmin()
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from u in context.Users
                             join uc in context.UserOperationClaims on u.Id equals uc.UserId
                             where uc.OperationClaimId == 2
                             select new UserProfileDto()
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

        public List<CarrierDto> GetAllCarriers()
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from u in context.Users
                             join uc in context.UserOperationClaims on u.Id equals uc.UserId
                             where uc.OperationClaimId == 3
                             select new CarrierDto()
                             {
                                 Email = u.Email,
                                 Name = u.FirstName + " " + u.LastName,
                                 Id = u.Id,
                                 PhoneNumber = u.PhoneNumber,
                                 Status = u.Status
                             };
                return result.ToList();
            }
        }

        public List<UserProfileDto> GetAllUserProfile()
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from u in context.Users
                             join b in context.Baskets on u.Id equals b.UserId into ps
                             from b in ps.DefaultIfEmpty()
                             join uc in context.UserOperationClaims on u.Id equals uc.Id
                             where uc.OperationClaimId == 1
                             select new UserProfileDto()
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

        public List<OperationClaim> GetClaims(UserDto user)
        {
            using (var context = new AvenSellContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join uoc in context.UserOperationClaims
                                 on operationClaim.Id equals uoc.OperationClaimId
                             where uoc.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();
            }
        }

        public UserDto GetDto(string email)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from u in context.Users
                             where u.Email == email
                             join b in context.Baskets on u.Id equals b.UserId into ps
                             from b in ps.DefaultIfEmpty()
                             select new UserDto()
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

        public UserProfileDto GetProfileDto(int userId)
        {
            using (AvenSellContext context = new AvenSellContext())
            {
                var result = from u in context.Users
                             where u.Id == userId
                             join b in context.Baskets on u.Id equals b.UserId into ps
                             from b in ps.DefaultIfEmpty()
                             select new UserProfileDto()
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
                return result.FirstOrDefault();
            }
        }
    }
}
