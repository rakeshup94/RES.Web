using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RES.Web.Models;
using RES.Web.Services.IServices;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RES.Web.Pages
{
    public class contactModel : PageModel
    {
        private readonly IMailService mailSrv;
        private readonly MailSettings _mailSettings;
        private readonly IWebHostEnvironment _hostEnvironment;
        public contactModel(IMailService _mailSrv, IOptions<MailSettings> mailSettings, IWebHostEnvironment hostEnvironment)
        {
            mailSrv = _mailSrv;
            _mailSettings = mailSettings.Value;
            this._hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public Customer SaleModel { get; set; }

        [BindProperty]
        public Customer QueryModel { get; set; }
        public void OnGet()
        {
        }



        public async Task<IActionResult> OnPostSales()
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            try
            {
                ModelState.Clear();
                if (TryValidateModel(SaleModel))
                {



                    string body = string.Empty;

                    string path = Path.Combine(wwwRootPath + "/template/", "emailer-query.html");
                    using (StreamReader reader = new StreamReader(path))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{Name}", SaleModel.Name);
                    body = body.Replace("{Email}", SaleModel.Email);
                    body = body.Replace("{PhoneNo}", SaleModel.PhoneNo);
                    body = body.Replace("{Message}", SaleModel.Message);


                    MailRequest _mail = new MailRequest();
                    _mail.Subject = "Sales Query";
                    _mail.ToEmail = _mailSettings.Mail;
                    _mail.Body = body;
                    _mail.SourcePath = _hostEnvironment.WebRootPath + "/Exception/";

                    await mailSrv.SendEmailAsync(_mail);
                    await ThanksMail(SaleModel.Name, SaleModel.Email);
                    return RedirectToPage("/Thanks");





                }
                else
                {
                    return Page();
                }
            }
            catch (Exception ex)
            {
                string path = _hostEnvironment.WebRootPath + "/Exception/";
                mailSrv.WriteException(path, ex);
                return RedirectToPage("/Error");
            }



        }

        public async Task<IActionResult> OnPostQuery()
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            try
            {
                ModelState.Clear();
                if (TryValidateModel(QueryModel))
                {
                    string body = string.Empty;

                    string path = Path.Combine(wwwRootPath + "/template/", "emailer-query.html");
                    using (StreamReader reader = new StreamReader(path))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{Name}", QueryModel.Name);
                    body = body.Replace("{Email}", QueryModel.Email);
                    body = body.Replace("{PhoneNo}", QueryModel.PhoneNo);
                    body = body.Replace("{Message}", QueryModel.Message);

                    MailRequest _mail = new MailRequest();
                    _mail.Subject = "Query";
                    _mail.ToEmail = _mailSettings.Mail;
                    _mail.Body = body;
                    await mailSrv.SendEmailAsync(_mail);
                    await ThanksMail(QueryModel.Name, QueryModel.Email);
                    return RedirectToPage("/Thanks");
                }
                else
                {
                    return Page();
                }

            }
            catch (Exception ex)
            {
                string path = _hostEnvironment.WebRootPath + "/Exception/";
                mailSrv.WriteException(path, ex);
                return Page();
            }
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
