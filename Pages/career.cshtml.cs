using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RES.Web.Models;
using RES.Web.Services.IServices;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace RES.Web.Pages
{
    public class careerModel : PageModel
    {

        [BindProperty]
        public Career CandidateModel { get; set; }
        public void OnGet()
        {
        }




        private readonly IMailService mailSrv;
        private readonly MailSettings _mailSettings;
        private readonly IWebHostEnvironment _hostEnvironment;
        public careerModel(IMailService _mailSrv, IOptions<MailSettings> mailSettings, IWebHostEnvironment hostEnvironment)
        {
            mailSrv = _mailSrv;
            _mailSettings = mailSettings.Value;
            this._hostEnvironment = hostEnvironment;
        }





        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Clear();
            if (TryValidateModel(CandidateModel))
            {

                MailRequest _mail = new MailRequest();
                if (CandidateModel.Resume != null)
                {
                    _mail.Attachments = new List<IFormFile>();
                    _mail.Attachments.Add(CandidateModel.Resume);
                }
                string body = string.Empty;
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string path = Path.Combine(wwwRootPath + "/template/", "emailer-career.html");
                using (StreamReader reader = new StreamReader(path))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Department}", CandidateModel.Department);
                body = body.Replace("{Email}", CandidateModel.Email);
                body = body.Replace("{PhoneNo}", CandidateModel.PhoneNo);
                body = body.Replace("{Experience}", CandidateModel.Experience);
                body = body.Replace("{Education}", CandidateModel.Education);
                body = body.Replace("{Project}", CandidateModel.Project);
                body = body.Replace("{Reference}", CandidateModel.Reference);


                _mail.Subject = "For  Career";
                _mail.ToEmail = _mailSettings.ToCC;
                _mail.Body = body;
                await mailSrv.SendEmailAsync(_mail);
                await ThanksMail(CandidateModel.Name, CandidateModel.Email);

            }
            else
            {
                return Page();
            }
            return RedirectToPage("/Thanks");
        }






        public async Task ThanksMail(string Name, string Email)
        {
            string body = string.Empty;
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string path = Path.Combine(wwwRootPath + "/template/", "emailer-thanyou.html");
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Name}", Name);
            MailRequest _mail = new MailRequest();
            _mail.Subject = "Thank you for Contacting Renewable Energy Systems Ltd";
            _mail.ToEmail = Email;
            _mail.Body = body;
            await mailSrv.SendEmailAsync(_mail);
        }






    }
}
