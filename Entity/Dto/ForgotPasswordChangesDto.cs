using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dto
{
    public class ForgotPasswordChangesDto:IDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        // public string Token { get; set; }
        public string Code { get; set; }
    }
}
