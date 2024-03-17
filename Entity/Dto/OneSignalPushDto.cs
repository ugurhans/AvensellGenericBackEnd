using System;
using Core;
using Core.Entities;

namespace Entity.Dtos
{
    public class OneSignalPushDto : IDto
    {
        public string Message { get; set; }
        public string Heading { get; set; }
        public List<string> Users { get; set; }
    }
}