using System.ComponentModel.DataAnnotations;

namespace RES.Web.Models
{
    public class Career
    {

        [Required, StringLength(50)]
        public string Designation { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }
        public string Resume { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string Education { get; set; }
        public string Project { get; set; }
        public string Reference { get; set; }
    }
}
