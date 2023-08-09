using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RES.Web.Models;
using RES.Web.Services.IServices;
using System.IO;
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
            if (!TryValidateModel(CandidateModel))
            {
                string body = string.Empty;
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string path = Path.Combine(wwwRootPath + "/template/", "emailer-query.html");
                using (StreamReader reader = new StreamReader(path))
                //{
                //    body = reader.ReadToEnd();
                ////}
                //body = body.Replace("{Name}", SaleModel.Name);
                //body = body.Replace("{Email}", SaleModel.Email);
                //body = body.Replace("{PhoneNo}", SaleModel.PhoneNo);
                //body = body.Replace("{Message}", SaleModel.Message);


                //MailRequest _mail = new MailRequest();
                //_mail.Subject = "Sales Query";
                //_mail.ToEmail = _mailSettings.Mail;
                //_mail.Body = body;
                //await mailSrv.SendEmailAsync(_mail);
                //await ThanksMail(SaleModel.Name, SaleModel.Email);
                return Page();
            }
            return RedirectToPage();
        }
    }
}
