namespace MultiStepFormApp.Web.Models
{
    public class FormData
    {
        public PersonalInfo PersonalInfo { get; set; } = new();
        public ContactInfo ContactInfo { get; set; } = new();
        public ProfessionalInfo ProfessionalInfo { get; set; } = new();
    }
}
