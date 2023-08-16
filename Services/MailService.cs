//using MimeKit;
//using MailKit.Net.Smtp;
//using MailKit.Security;

using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;
using System;
using RES.Web.Models;
using RES.Web.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace RES.Web.Services
{
    public class MailService : IMailService
    {

        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }



        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName);
            message.To.Add(new MailAddress(mailRequest.ToEmail));

            if (!string.IsNullOrEmpty(_mailSettings.ToCC))
            {
                message.CC.Add(new MailAddress(_mailSettings.ToCC));
            }

            message.Subject = mailRequest.Subject;
            message.Body = mailRequest.Body;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_mailSettings.Mail.Trim(), _mailSettings.Password.Trim(), "resindia.co.in");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = _mailSettings.Host;
            client.Port = _mailSettings.Port;
            client.EnableSsl = true;

            if (mailRequest.Attachments != null)
            {
                foreach (var file in mailRequest.Attachments)
                {
                    string fileName = file.FileName;
                    long length = file.Length;
                    if (length > 0)
                    {
                        FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
                        byte[] bytes = new byte[length];
                        fileStream.Read(bytes, 0, (int)file.Length);
                        message.Attachments.Add(new Attachment(fileStream, fileName));

                    }
                }
            }



            try
            {

                await client.SendMailAsync(message);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
