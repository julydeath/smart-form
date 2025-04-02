using Microsoft.AspNetCore.Mvc;
using MultiStepFormApp.Web.Models;
using System.Diagnostics;
using System.Text.Json;


namespace MultiStepFormApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Clear any previous session data
            HttpContext.Session.Clear();
            return View();
        }

        // Step 1: Personal Information
        public IActionResult Step1()
        {
            // If we have session data, use it to populate the form
            var personalInfoJson = HttpContext.Session.GetString("PersonalInfo");
            if (!string.IsNullOrEmpty(personalInfoJson))
            {
                return View(JsonSerializer.Deserialize<PersonalInfo>(personalInfoJson));
            }
            return View(new PersonalInfo());
        }

        [HttpPost]
        public IActionResult Step1(PersonalInfo personalInfo)
        {
            Console.WriteLine("== Step1 POST ==");
            Console.WriteLine("FirstName: " + personalInfo.FirstName);
            Console.WriteLine("LastName: " + personalInfo.LastName);
            Console.WriteLine("DOB: " + personalInfo.DateOfBirth);
            Console.WriteLine("IsValid: " + ModelState.IsValid);

            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("PersonalInfo", JsonSerializer.Serialize(personalInfo));
                return RedirectToAction("Step2");
            }

            return View(personalInfo);
        }

        // Step 2: Contact Information
        public IActionResult Step2()
        {
            // Check if step 1 was completed
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("PersonalInfo")))
            {
                return RedirectToAction("Step1");
            }

            var contactInfoJson = HttpContext.Session.GetString("ContactInfo");
            if (!string.IsNullOrEmpty(contactInfoJson))
            {
                return View(JsonSerializer.Deserialize<ContactInfo>(contactInfoJson));
            }
            return View(new ContactInfo());
        }

        [HttpPost]
        public IActionResult Step2(ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("ContactInfo", JsonSerializer.Serialize(contactInfo));
                return RedirectToAction("Step3");
            }
            return View(contactInfo);
        }

        // Step 3: Professional Information
        public IActionResult Step3()
        {
            // Check if step 2 was completed
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("ContactInfo")))
            {
                return RedirectToAction("Step2");
            }

            var professionalInfoJson = HttpContext.Session.GetString("ProfessionalInfo");
            if (!string.IsNullOrEmpty(professionalInfoJson))
            {
                return View(JsonSerializer.Deserialize<ProfessionalInfo>(professionalInfoJson));
            }
            return View(new ProfessionalInfo());
        }

        [HttpPost]
        public IActionResult Step3(ProfessionalInfo professionalInfo)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("ProfessionalInfo", JsonSerializer.Serialize(professionalInfo));
                return RedirectToAction("FormResult");
            }
            return View(professionalInfo);
        }

        // Form Result Page
        public IActionResult FormResult()
        {
            // Check if all steps were completed
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("PersonalInfo")) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString("ContactInfo")) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString("ProfessionalInfo")))
            {
                return RedirectToAction("Index");
            }

            var formData = new FormData
            {
                PersonalInfo = JsonSerializer.Deserialize<PersonalInfo>(HttpContext.Session.GetString("PersonalInfo")),
                ContactInfo = JsonSerializer.Deserialize<ContactInfo>(HttpContext.Session.GetString("ContactInfo")),
                ProfessionalInfo = JsonSerializer.Deserialize<ProfessionalInfo>(HttpContext.Session.GetString("ProfessionalInfo"))
            };

            return View(formData);
        }

        // API endpoint to get form data in JSON format
        [HttpGet]
        [Route("api/form-data")]
        public IActionResult GetFormDataJson()
        {
            // Make sure all form steps are completed
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("PersonalInfo")) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString("ContactInfo")) ||
                string.IsNullOrEmpty(HttpContext.Session.GetString("ProfessionalInfo")))
            {
                return BadRequest(new { error = "Form is incomplete" });
            }

            var formData = new FormData
            {
                PersonalInfo = JsonSerializer.Deserialize<PersonalInfo>(HttpContext.Session.GetString("PersonalInfo")),
                ContactInfo = JsonSerializer.Deserialize<ContactInfo>(HttpContext.Session.GetString("ContactInfo")),
                ProfessionalInfo = JsonSerializer.Deserialize<ProfessionalInfo>(HttpContext.Session.GetString("ProfessionalInfo"))
            };

            return Json(formData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}