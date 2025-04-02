// using System.ComponentModel.DataAnnotations;

// namespace MultiStepFormApp.Web.Models
// {
//     public class ProfessionalInfo
//     {
//         [Required(ErrorMessage = "Occupation is required")]
//         [Display(Name = "Occupation")]
//         public string Occupation { get; set; } = string.Empty;

//         [Required(ErrorMessage = "Company is required")]
//         [Display(Name = "Company")]
//         public string Company { get; set; } = string.Empty;

//         [Required(ErrorMessage = "Years of experience is required")]
//         [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50")]
//         [Display(Name = "Years of Experience")]
//         public int YearsOfExperience { get; set; }
//     }
// }

using System.ComponentModel.DataAnnotations;

namespace MultiStepFormApp.Web.Models
{
    public class ProfessionalInfo
    {
        [Required]
        public string Occupation { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        [Range(0, 50)]
        public int YearsOfExperience { get; set; }
    }
}
