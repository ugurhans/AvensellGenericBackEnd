using System;
using Core;
using Core.Entities;

namespace Entity.Dtos
{
    public class INotificationDetailType : IDto
    {
        public int Id { get; set; }
        public List<IUserNotificationType> Users { get; set; }
        public string Header { get; set; }
        public string Desc { get; set; }
        public string CreatedDate { get; set; }
    }
}