using Core;

namespace Entity.Dtos;

public class IUserNotificationType : IDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}