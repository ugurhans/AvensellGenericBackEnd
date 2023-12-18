using System;
using Core;

namespace Entity.Dto
{
    public class MailSettings : IDto
    {

        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

    }
}

