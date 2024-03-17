using Core.Entities;

namespace Entity.Concrate;

public class UserNotification : IEntity
{
    public int ID { get; set; }
    public int NotificationID { get; set; }
    public int UserID { get; set; }

    public virtual Notification Notification { get; set; }
}