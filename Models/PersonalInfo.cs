using System;
using System.ComponentModel.DataAnnotations;

namespace MultiStepFormApp.Web.Models
{
    public class PersonalInfo
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
