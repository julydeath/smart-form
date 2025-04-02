using System.ComponentModel.DataAnnotations;

namespace MultiStepFormApp.Web.Models
{
    public class ContactInfo
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
