using System.ComponentModel.DataAnnotations;

namespace RES.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int FormId { get; set; }

        [Required, StringLength(4)]
        public string Name { get; set; }

        [Required, StringLength(5)]
        public string Email { get; set; }
        [Required, StringLength(5)]
        public string PhoneNo { get; set; }
        public string Message { get; set; }



    }
}
