using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Core.Utilities.Results;
using Entity.DTOs;
using Entity.Dto;
using System.Numerics;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public UserDto GetByMailDto(string email)
        {
            return _userDal.GetDto(email);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }


        public IResult Delete(int userId)
        {
            _userDal.Delete(userId);
            return new SuccessResult("Başarıyla Silindi.");
        }

        public List<OperationClaim> GetClaims(UserDto user)
        {
            return _userDal.GetClaims(user);
        }

        //public List<OperationClaim> GetClaims(User user)//
        //{
        //    return _userDal.GetClaims(user);
        //}

        public IResult UpdateMail(User user)
        {
            var userExist = _userDal.GetDto(user.Email);
            if (userExist == null)
            {
                _userDal.Update(user);
                return new SuccessResult(Messages.Updated);
            }
            return new ErrorResult("Email Sisteme Zaten Kayıtlı");
        }

        public IDataResult<UserProfileDto>? GetUserProfile(int userId)
        {
            return new SuccessDataResult<UserProfileDto>(_userDal.GetProfileDto(userId));
        }
      
        public IDataResult<int> GetAllCount()
        {
            return new SuccessDataResult<int>(_userDal.GetAll().Count);
        }


        public IDataResult<List<UserProfileDto>> GetAllUserProfile()
        {
            return new SuccessDataResult<List<UserProfileDto>>(_userDal.GetAllUserProfile());
        }

        public IResult Update(User user)
        {
            var userExist = _userDal.GetDto(user.Email);
            if (userExist == null)
            {
                return new ErrorResult("Email Sisteme Zaten Kayıtlı");
            }
            _userDal.Update(user);
            return new SuccessResult(Messages.Updated);
        }

        public IResult UpdateDto(UpdateUserDto userDto)
        {
            var userExist = _userDal.Get(x => x.Id == userDto.Id);
            if (userExist == null)
            {
                return new ErrorResult("Hesap Bulunamadı.Destekle iletişime geçiniz.");
            }
            else
            {
                userExist.PhoneNumber = userDto.Phone;
                userExist.Email = userDto.Email;
                userExist.LastName = userDto.LastName;
                userExist.FirstName = userDto.FirstName;
                userExist.ModifiedDate = DateTime.Now;
            }
            _userDal.Update(userExist);
            return new SuccessResult(Messages.Updated);
        }

        public IDataResult<List<CarrierDto>> GetAllCarriers()
        {
            return new SuccessDataResult<List<CarrierDto>>(_userDal.GetAllCarriers());
        }
        //
        public IDataResult<UserProfileDto>? GetById(int id)
        {
            return new SuccessDataResult<UserProfileDto>(_userDal.GetProfileDto(id));
        }

      
    }
}
