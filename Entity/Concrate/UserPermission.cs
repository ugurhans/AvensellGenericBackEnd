using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class UserPermission : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Sms { get; set; }
        public bool Notification { get; set; }
        public bool EMail { get; set; }
    }
}

