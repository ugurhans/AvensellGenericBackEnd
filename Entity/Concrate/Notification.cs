using System;
using Core.Entities;

namespace Entity.Concrate
{
    public class Notification : IEntity
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}