using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    public class NotificationUsersController : Controller
    {
        private readonly IOneSignalService _oneSignalService;
        public NotificationUsersController(IOneSignalService oneSignalService)
        {
            _oneSignalService = oneSignalService;
        }

        [HttpGet("SendPushToUsers")]
        public IActionResult SendPushToUsers(OneSignalPushDto oneSignalPushDto)
        {
            var result = _oneSignalService.SendPushAppAsync(oneSignalPushDto);
            return Ok(result);
        }

        [HttpGet("GetAllNotifications")]
        public IActionResult GetAllNotifications()
        {
            using var context = new AvenSellContext();
            var notifications = context.Notifications.Include("UserNotifications").ToList();
            var result = new List<INotificationDetailType>();

            foreach (var notification in notifications)
            {
                var users = notification.UserNotifications.Select(un =>
                {
                    var matchingUser = context.Users.FirstOrDefault(user => user.Id == un.UserID);
                    if (matchingUser != null)
                    {
                        return new IUserNotificationType
                        {
                            Id = un.UserID,
                            FirstName = matchingUser.FirstName,
                            LastName = matchingUser.LastName
                        };
                    }
                    else
                    {
                        return null; // Eşleşen kullanıcı bulunamazsa isteğe bağlı olarak null veya başka bir değer dönebilirsiniz.
                    }
                }).Where(user => user != null).ToList();

                var notificationDetail = new INotificationDetailType
                {
                    Id = notification.ID,
                    Users = users,
                    Header = notification.Header,
                    Desc = notification.Description,
                    CreatedDate = notification.CreatedDate.ToString(),
                    // Diğer gerekli bilgiler
                };

                result.Add(notificationDetail);
            }

            return Ok(new SuccessDataResult<List<INotificationDetailType>>(result));
        }

    }
}

