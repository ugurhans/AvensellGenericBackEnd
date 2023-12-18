using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {

            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("enes0303enes@gmail.com");  // Buraya gönderen kişinin e-posta adresini ekleyin
                msg.To.Add(toEmail);  // Alıcının e-posta adresini ekleyin
                msg.Subject = subject;
                msg.Body = body;

                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("enes0303enes@gmail.com", "mrvntwvxvsakygqe");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    await client.SendMailAsync(msg);
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
