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

        public ResetPasswordCodeManager(IResetPasswordCodeDal resetPasswordCodeDal, IUserService userService)
        {
            _resetPasswordCodeDal = resetPasswordCodeDal;
            _userService = userService;
        }
    }
}
