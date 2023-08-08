using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RES.Web.Models;
using System.Threading.Tasks;

namespace RES.Web.Pages
{
    public class ContactModel : PageModel
    {

        [BindProperty]
        public Customer Customer { get; set; }

        //[BindProperty]
        //public Customer Query { get; set; }
        public void OnGet()
        {
        }



        public IActionResult OnPostSales()
        {

            var ss = Customer;

            if (!ModelState.IsValid)
            {

                return Page();
            }

            return RedirectToPage();
        }


        public IActionResult OnPost()
        {
            var ss = Customer;

            if (!ModelState.IsValid)
            {





                return Page();
            }

            return RedirectToPage();

        }












    }
}
