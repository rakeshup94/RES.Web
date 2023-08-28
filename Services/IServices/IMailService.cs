using RES.Web.Models;
using System;
using System.Threading.Tasks;

namespace RES.Web.Services.IServices
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        void WriteException(string filePath, Exception ex);

    }
}
