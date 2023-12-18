using Business.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Core.Utilities.Results;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Entity.Request;
using Entity.Dto;

namespace Business.Concrate
{
    public class MailManager : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IUserService _userService;

        public MailManager(IOptions<MailSettings> mailSettings, IUserService userService)
        {
            _userService = userService;
            _mailSettings = mailSettings.Value;
        }

        public async Task<IResult> SendLostEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            StreamReader reader = new StreamReader(path: "C:\\inetpub\\wwwroot\\sites\\KadimSite\\Api\\Assets\\Mail\\lostPass.html");

            string readfile = reader.ReadToEnd();
            string StrContent;

            StrContent = readfile;
            var user = _userService.GetByMail(mailRequest.ToEmail);
            if (user != null)
            {
                Random random = new Random();
                user.LostPin = random.Next(1000, 99999).ToString();
                _userService.Update(user);
                StrContent = StrContent.Replace("##PINNUMBER##", user.LostPin);

                builder.HtmlBody = StrContent;
                builder.TextBody = mailRequest.Body;

                email.Body = builder.ToMessageBody();

                builder.HtmlBody = StrContent;
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            return new SuccessResult("Mail Başarıyla Gönderildi.");
        }

        public async Task<IResult> SendResetEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            StreamReader reader = new StreamReader(path: "C:\\inetpub\\wwwroot\\sites\\KadimSite\\Api\\Assets\\Mail\\resetPass.html");

            string readfile = reader.ReadToEnd();
            string StrContent;

            StrContent = readfile;
            var user = _userService.GetByMail(mailRequest.ToEmail);
            if (user != null)
            {
                Random random = new Random();
                user.LostPin = random.Next(1000, 99999).ToString();
                _userService.Update(user);
                StrContent = StrContent.Replace("##PINNUMBER##", user.LostPin);

                builder.HtmlBody = StrContent;
                builder.TextBody = mailRequest.Body;

                email.Body = builder.ToMessageBody();

                builder.HtmlBody = StrContent;
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            return new SuccessResult("Mail Başarıyla Gönderildi.");
        }
    }
}

