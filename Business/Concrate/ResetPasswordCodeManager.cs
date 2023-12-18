using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class ResetPasswordCodeManager : IResetPasswordCodeService
    {

        private readonly IResetPasswordCodeDal _resetPasswordCodeDal;
        private readonly IUserService _userService;
        private readonly RandomCodeGenerator _randomCodeGenerator;

        public ResetPasswordCodeManager(IResetPasswordCodeDal resetPasswordCodeDal, IUserService userService,RandomCodeGenerator randomCodeGenerator)
        {
            _resetPasswordCodeDal = resetPasswordCodeDal;
            _userService = userService;
            _randomCodeGenerator = randomCodeGenerator;
        }
        public void CreateResetPasswordCode(int userId)
        {
            _resetPasswordCodeDal.Delete(userId);

            // Kullanıcının şifre sıfırlama kodunu oluştur
            string resetCode = RandomCodeGenerator();

            // Şifre sıfırlama kodunun geçerlilik süresini 10 dakika olarak ayarla
            DateTime expirationDate = DateTime.Now.AddMinutes(10);

            // Yeni şifre sıfırlama kodunu tabloya ekle
            _resetPasswordCodeDal.Add(new ResetPasswordCode
            {
                UserId = userId,
                Code = resetCode,
                ExpirationDate = expirationDate
            });

            // Şifre sıfırlama kodunu kullanıcıya gönder (örneğin, e-posta ile)
            SendResetCodeToUser(userId, resetCode);
        }
    }
}
