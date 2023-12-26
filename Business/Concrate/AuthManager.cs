using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entity.DTOs;
using DataAccess.Abstract;
using Entity.Dto;
using Entity.Concrate;
using MimeKit;
using System.Net.Mail;

namespace Business.Concrete
{

    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserOperationClaimsDal _userOperationClaimsDal;
        private readonly IResetPasswordCodeDal _resetpasswordCodeDal;
        private readonly IMailService _mailService;



        public AuthManager(IUserOperationClaimsDal userOperationClaimsDal, ITokenHelper tokenHelper, IUserService userService, IResetPasswordCodeDal resetpasswordCodeDal, IMailService mailService)
        {
            _userOperationClaimsDal = userOperationClaimsDal;
            _tokenHelper = tokenHelper;
            _userService = userService;
            _resetpasswordCodeDal = resetpasswordCodeDal;
            _mailService = mailService;
        }

        public IDataResult<UserDto> CreateAccessToken(UserDto user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            user.Token = accessToken;
            return new SuccessDataResult<UserDto>(user, Messages.AccessTokenCreated);
        }

        public IResult Delete(int userId)
        {
            _userService.Delete(userId);
            _userOperationClaimsDal.DeleteRange(userId);

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

        private string GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 10000); // 1000 ile 9999 arasında rastgele bir sayı üret

            return code.ToString("D4"); // Kodu 4 haneli bir dize olarak döndür
        }


        public async Task<IResult> SendPasswordResetMailAsync(string userMail)
        {
            var code = GenerateRandomCode();

            var result = await _mailService.SendLostEmailAsync(new Entity.Request.MailRequest() { Body = "Deneme", Subject = "Deneme", ToEmail = userMail });
            if (result.Success)
            {

                return new SuccessResult(result.Message);
            }
            return new ErrorResult(result.Message);
        }

        public IResult ChangePassword(string userMail, string code, string newPassword)
        {
            var user = _userService.GetByMail(userMail);
            if (user != null && user.LostPin != null && user.LostPin.Length > 0)
            {
                var pin = user.LostPin;
                if (pin == code)
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.LostPin = null;
                    _userService.Update(user);
                    return new SuccessResult("User Password Changed");
                }
                return new ErrorResult("Code is not correct");
            }
            return new ErrorResult("User Or Code not found");

        }
    }
}