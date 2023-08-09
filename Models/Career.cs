using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RES.Web.Models
{
    public class Career
    {

        [Required, StringLength(50)]
        public string Department { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Phone No.")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [RegularExpression(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)", ErrorMessage = "Not a valid phone number")]       [StringLength(15, MinimumLength = 10)]
        public string PhoneNo { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public string Project { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public IFormFile Resume { get; set; }




    }
}
