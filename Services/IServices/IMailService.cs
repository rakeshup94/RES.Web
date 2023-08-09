using RES.Web.Models;
using System.Threading.Tasks;

namespace RES.Web.Services.IServices
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
