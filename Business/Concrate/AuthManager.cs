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
using Core.Entities.Dtos;
using Entity.Request;

namespace Business.Concrete
{

    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserOperationClaimsDal _userOperationClaimsDal;
        private readonly IResetPasswordCodeDal _resetpasswordCodeDal;
        private readonly IMailService _mailService;
        private IAdminService _adminService;
        private readonly IMailOtpCodeDal _mailOtpCodeDal;


        public AuthManager(IUserOperationClaimsDal userOperationClaimsDal, ITokenHelper tokenHelper, IUserService userService, IResetPasswordCodeDal resetpasswordCodeDal, IMailService mailService, IAdminService adminService, IMailOtpCodeDal mailOtpCodeDal)
        {
            _userOperationClaimsDal = userOperationClaimsDal;
            _tokenHelper = tokenHelper;
            _userService = userService;
            _resetpasswordCodeDal = resetpasswordCodeDal;
            _mailService = mailService;
            _adminService = adminService;
            _mailOtpCodeDal = mailOtpCodeDal;
        }

        public IDataResult<UserDto> CreateAccessToken(UserDto user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            user.Token = accessToken;
            return new SuccessDataResult<UserDto>(user, Messages.AccessTokenCreated);
        }
        public IDataResult<AdminDto> CreateAccessTokenAdmin(AdminDto admin)
        {
            var claims = _adminService.GetClaims(admin);
            var accessToken = _tokenHelper.CreateToken(admin, claims);
            admin.Token = accessToken;
            return new SuccessDataResult<AdminDto>(admin, Messages.AccessTokenCreated);
        }

        public IResult Delete(int userId)
        {
            _userService.Delete(userId);
            _userOperationClaimsDal.DeleteRange(userId);

            return new SuccessResult();
        }


        private IResult CheckOtpCode(string otpCode, int userId)
        {
            var code = _mailOtpCodeDal.Get(otp=>otp.UserId == userId && otp.Verified == false);
            if (code != null)
            {
                if (code.CreatedDate.AddSeconds(code.LifeTimeSecond) <  DateTime.Now)
                {
                    return new ErrorResult("Kod Artık Geçersiz");
                }
                else if  (code.OtpCode != otpCode)
                {
                    return new ErrorResult("Kod Hatalı");
                }

                code.Verified = true;
                _mailOtpCodeDal.Update(code);
                return new SuccessResult("Doğrulama Başarılı");
            }
            return new ErrorResult("Kod Bulunamadı");

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

            var otpResult = CheckOtpCode(userForLoginDto.OtpCode, userToCheck.Id);
            if (!otpResult.Success)
            {
                return new ErrorDataResult<UserDto>(otpResult.Message);
            }
            
            return new SuccessDataResult<UserDto>(userToCheck, "Giriş Başarıyla Sağlandı.");
        }

        public IDataResult<AdminDto> LoginAdmin(AdminForLoginDto adminForLoginDto)
        {
            var userToCheck = _adminService.GetByMailDtoAdmin(adminForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<AdminDto>("E-posta'ya ait bir kayıt bulunamadı.");
            }
            if (!HashingHelper.VerifyPasswordHash(adminForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<AdminDto>(Messages.Error);
            }
            return new SuccessDataResult<AdminDto>(userToCheck, "Giriş Başarıyla Sağlandı.");
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

        public IDataResult<AdminDto> RegisterAdmin(AdminForRegisterDto adminForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var admin = new Admin
            {
                Email = adminForRegisterDto.Email,
                FirstName = adminForRegisterDto.FirstName,
                LastName = adminForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                PhoneNumber = adminForRegisterDto.Phone,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                LastLogin = DateTime.Now
            };
            _adminService.Add(admin);
         
            var dto = _adminService.GetByMailDtoAdmin(admin.Email);
            return new SuccessDataResult<AdminDto>(dto, "Başarıyla Kayıt Olundu.");
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

        public async Task<IResult> SendOtpMail(string userMail)
        {
            var user = _userService.GetByMail(userMail);
            if (user != null)
            {
                var otps = _mailOtpCodeDal.GetAll(otp=>otp.UserId == user.Id && otp.Verified == false);
                if (otps.Count > 0)
                {
                    foreach (var mailOtpCode in otps)
                    {
                        mailOtpCode.Verified = true;
                        _mailOtpCodeDal.Update(mailOtpCode);
                    }
                }
                var mailResult =  await _mailService.SendOtpMail(new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "OtpCode",
                    Body = "OtpCode"
                });
                if (mailResult.Success)
                {
                    return new SuccessResult(mailResult.Message);
                }
                return new ErrorResult(mailResult.Message);
            }

            return new ErrorResult("Kullanıcı Bulunamadı");
        }
    }
}