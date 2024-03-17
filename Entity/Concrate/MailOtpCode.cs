using Core.Entities;

namespace Entity.Concrate;

public class MailOtpCode : IEntity
{
    public int Id { get; set; }
    public string OtpCode { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public int LifeTimeSecond { get; set; }
    public bool Verified { get; set; }
}