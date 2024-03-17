using System;
using System.Net;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrate;
using Entity.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RestSharp;
using Twilio.TwiML.Messaging;

namespace Business.Concrate
{
    public class OneSignalManager : IOneSignalService
    {
        private readonly IUserNotificationDal _userNotificationDal;
        private readonly INotificationDal _notificationDal;

        public OneSignalManager(IUserNotificationDal userNotificationDal, INotificationDal notificationDal)
        {
            _userNotificationDal = userNotificationDal;
            _notificationDal = notificationDal;
        }

        public async Task<IResult> SendPushAppAsync(OneSignalPushDto oneSignalPushDto)
        {
            string apiKey = "MGFmY2U1ZmItMjg4YS00N2MwLWFjNWUtMmNlMjkxNzMxYTMx";
            string appId = "733459c8-5acc-42f9-a37b-48a42308cf90";

            string responseCollection = "";
            foreach (var item in oneSignalPushDto.Users)
            {
                var request = (HttpWebRequest)WebRequest.Create("https://onesignal.com/api/v1/notifications");
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json; charset=UTF-8";
                request.Headers.Add("Authorization", $"Basic {apiKey}");

                var filters = oneSignalPushDto.Users.Select(user => new
                {
                    field = "tag",
                    key = "TITIZ_USERID",
                    relation = "=",
                    value = item,
                }).ToArray();


                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var json = new
                    {
                        app_id = appId,
                        filters = filters,
                        data = new
                        {
                            foo = "bar"
                        },
                        contents = new
                        {
                            en = oneSignalPushDto.Message
                        }
                    };
                    streamWriter.Write(JsonConvert.SerializeObject(json));
                }

                var response = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    responseCollection += result.ToString();
                }
            }


            var notification = new Notification
            {
                Header = oneSignalPushDto.Heading,
                Description = oneSignalPushDto.Message,

                CreatedDate = DateTime.Now,
            };
            _notificationDal.Add(notification);

            foreach (var user in oneSignalPushDto.Users)
            {
                var userNotification = new UserNotification
                {
                    NotificationID = notification.ID,
                    UserID = Convert.ToInt32(user),
                };

                _userNotificationDal.Add(userNotification);
            }

            return new SuccessResult(message: responseCollection);

        }
    }
}

