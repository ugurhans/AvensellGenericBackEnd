﻿using System;
using Core;

namespace Entity.Dto
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string OtpCode { get; set; }
    }
}

