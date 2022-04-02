using Qalam.BLL.ViewModels;
using Qalam.Common.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Qalam.BLL.Services
{
    public class MailService
    {
        public static readonly string SMTP_USER = "@gmail.com";
        public static readonly string PASSWORD = "=";
        public static readonly string SMTP_SERVER = "smtp.gmail.com";
        public static readonly int SMTP_PORT = 587;

        public async Task SendAsync(MailMessageViewModel message)
        {
            try
            {
                var client = new SmtpClient(SMTP_SERVER, SMTP_PORT)
                {
                    Credentials = new NetworkCredential(SMTP_USER, Protected.Decrypt(PASSWORD)),
                    EnableSsl = true
                };

                await client.SendMailAsync(new MailMessage(SMTP_USER, message.To)
                {
                    IsBodyHtml = message.IsBodyHtml,
                    Body = message.Body,
                    Subject = message.Subject
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
