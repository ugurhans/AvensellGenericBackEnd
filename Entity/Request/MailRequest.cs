
using System;
using Core;

namespace Entity.Request
{
    public class MailRequest : IDto
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}

