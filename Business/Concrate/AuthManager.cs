using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using Entity.Dto;
using System.IdentityModel.Tokens.Jwt;
using Business.Concrate;
using Entity.Concrate;

namespace Business.Concrete
{

    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserOperationClaimsDal _userOperationClaimsDal;
        private IEmailService _emailService;
        private readonly RandomCodeGenerator _codeGenerator;



        public AuthManager(IUserOperationClaimsDal userOperationClaimsDal, ITokenHelper tokenHelper, IUserService userService, IEmailService emailService,RandomCodeGenerator randomCodeGenerator)
        {
            _userOperationClaimsDal = userOperationClaimsDal;
            _tokenHelper = tokenHelper;
            _userService = userService;
            _emailService = emailService;
            _codeGenerator = randomCodeGenerator;
           
        }

        public IDataResult<UserDto> CreateAccessToken(UserDto user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            user.Token = accessToken;
            return new SuccessDataResult<UserDto>(user, Messages.AccessTokenCreated);
        }

        //public IDataResult<User> ForgotPassword(ResertPasswordDto resertPasswordDto)
        //{
        //    var user = _userService.GetByMail(resertPasswordDto.Email);

        //    if (user != null && resertPasswordDto.NewPassword == resertPasswordDto.NewPasswordAgain && resertPasswordDto.LostPin == user.LostPin)
        //    {
        //        byte[] passwordHash, passwordSalt;
        //        HashingHelper.CreatePasswordHash(resertPasswordDto.NewPassword, out passwordHash, out passwordSalt);

        //        user.PasswordHash = passwordHash;
        //        user.PasswordSalt = passwordSalt;
        //        user.LostPin = "";
        //        _userService.Update(user);
        //        return new SuccessDataResult<User>(user, "Şifre Başarıyla Sıfırlandı");
        //    }

        //    return new ErrorDataResult<User>("E-posta'ya ait bir kayıt bulunamadı.");
        //}

        public IResult Delete(int userId)
        {
            _userService.Delete(userId);
            _userOperationClaimsDal.DeleteRange(userId);
            //var basketId = _basketDal.Get(b => b.UserId == userId).Id;
            //_basketDal.Delete(basketId);
            //_basketItemDal.DeleteRange(basketId);
            //_userFavoriteDal.DeleteRange(userId);
            //_addressDal.DeleteRange(userId);
            return new SuccessResult();
        }

        public IDataResult<UserDto> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMailDto(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<UserDto>("E-posta'ya ait bir kayıt bulunamadı.");
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<UserDto>(Messages.Error);
            }
            //userToCheck.BasketId = _basketDal.Get(b => b.UserId == userToCheck.Id).Id;
            return new SuccessDataResult<UserDto>(userToCheck, "Giriş Başarıyla Sağlandı.");
        }


        public IDataResult<UserDto> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                PhoneNumber = userForRegisterDto.Phone,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                LastLogin = DateTime.Now
            };
            _userService.Add(user);
            _userOperationClaimsDal.Add(new UserOperationClaim
            {
                UserId = user.Id,
                OperationClaimId = 1
            });
            var dto = _userService.GetByMailDto(user.Email);
            return new SuccessDataResult<UserDto>(dto, "Başarıyla Kayıt Olundu.");
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.AlreadyExist);
            }
            return new SuccessResult();
        }


        //public IDataResult<bool> ForgotPasswordVerifyToken(ForgotPasswordChangesDto password)
        //{
        //    byte[] passwordHash, passwordSalt;
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.ReadJwtToken(password.Token);
        //    var emailClaim = token.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        //    if (string.IsNullOrEmpty(emailClaim))
        //        return new ErrorDataResult<bool>(false, Messages.EmailAddressNotFound);

        //    User user = _userService.GetByMail(emailClaim);
        //    if (user == null)
        //        return new ErrorDataResult<bool>(false, Messages.UserNotFound);


        //    if (user.Email == emailClaim)
        //    {
        //        HashingHelper.CreatePasswordHash(password.Password, out passwordHash, out passwordSalt);
        //        user.PasswordHash = passwordHash;
        //        user.PasswordSalt = passwordSalt;
        //        _userService.Update(user);
        //    }

        //    return new SuccessDataResult<bool>(true, Messages.PasswordChanges);
        //}

        public IDataResult<bool> ForgotPasswordVerifyToken(ForgotPasswordChangesDto password)//methot ismi code yap.
        {
            // Kullanıcının email'ine göre kullanıcı bilgileri alınıyor.
            User user = _userService.GetUserProfile(password);
            // User user = _userService.GetUserByResetCode(password.Code);

            // Eğer kullanıcı bulunamazsa hata döndürülüyor.
            if (user == null)
                return new ErrorDataResult<bool>(false, Messages.UserNotFound);

            // Kullanıcıya gönderilen şifre sıfırlama kodu ile gelen kodu karşılaştırıyor.
            if (ResetPasswordCode. != password.Code)
                return new ErrorDataResult<bool>(false, Messages.Deleted);

            // Yeni şifrenin hash ve salt'ı oluşturuluyor.
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password.Password, out passwordHash, out passwordSalt);

            // Kullanıcının bilgileri güncelleniyor.
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Şifre sıfırlama kodu kullanıldığı için sıfırlanıyor.
            user.ResetPasswordCode = null;

            _userService.Update(user);

            // İşlem başarılı olursa true değeri ile birlikte başarı mesajı döndürülüyor.
            return new SuccessDataResult<bool>(true, Messages.PasswordChanges);
        }



        public async Task<IDataResult<string>> ForgotPasswordSendLink(ForgotPasswordSendLinkDto email)
        {
            var user = _userService.GetByMail(email.Email);
            if (user != null)
            {
                //var userclaim = _userService.GetClaims(user);
                //AccessToken token = _tokenHelper.CreateToken(user, userclaim);
                string generatedCode = _codeGenerator.GenerateCode(4);
                _userService.Update(user);
                string toEmail = email.Email; // Alıcı e-posta adresi
                string subject = "Şifre Sıfırlama"; // E-posta konusu
                string resetcode = $"{generatedCode}";


                string body = $"Merhaba, şifrenizi sıfırlamak için aşağıdaki kodu kullanabilirsiniz: {resetcode} ";

                bool emailSent = await _emailService.SendEmailAsync(toEmail, subject, body);
                if (emailSent)
                    return new SuccessDataResult<string>(user.Email, Messages.EmailSent);
            }

            return new ErrorDataResult<string>(Messages.EmailNotBeSent);


        }
       


    }

}