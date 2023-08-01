using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RES.Web.Pages
{
    public class contactsModel : PageModel
    {
        public void OnGet()
        {
        }


        public void OnPost()
        {
            //var name = FullName;
            //var email = Email;
            //var mobile = ContactNumbers;
            // now you have all submitted values in local variables


            //var name = Request.Form["FullName"];
            //var email = Request.Form["Email"];
            //var mobile = Request.Form["ContactNumbers"];
            // now you have all submitted values in local variables
        }



        public void OnPostRegisterAsync()
        {
            var name = Request.Form["FullName"];
            // now you have all submitted values in local variables
        }


        public void OnPostRegister()
        {
            var name = Request.Form["FullName"];
            // now you have all submitted values in local variables
        }
        public void OnPostRequest()
        {
            var name = Request.Form["FullName"];
            // now you have all submitted values in local variables
        }


        public void OnPostAspnet(int valuecount)
        {
            ViewData["ActionMessage"] = $"Aspnet {valuecount}";
        }
        public void OnPostAspnetmvc(int valuecount)
        {
            ViewData["ActionMessage"] = $"AspnetMVC {valuecount}";
        }
        public void OnPostAspnetcore(int valuecount)
        {
            ViewData["ActionMessage"] = $"AspnetCore {valuecount}";
        }
        public void OnPostLinq(int valuecount)
        {
            ViewData["ActionMessage"] = $"LINQ {valuecount}";
        }
        public void OnPostHtml(int valuecount)
        {
            ViewData["ActionMessage"] = $"Html {valuecount}";
        }
    }
}
