using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Text.Json;
using MultiStepFormApp.Web.Models;

namespace MultiStepFormApp.Web.Services
{
    public class FormAutomationService
    {
        private void WaitForElement(IWebDriver driver, By by, int timeoutSeconds = 10)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            wait.Until(driver => driver.FindElement(by));
        }
        private readonly string _baseUrl;

        public FormAutomationService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public static FormData LoadConfigFromFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<FormData>(json);
        }

        public void SubmitForm(FormData formData)
        {
            var options = new ChromeOptions();
            // options.AddArgument("--headless");

            using var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl($"{_baseUrl}/Home/Step1");

            // Step 1
            WaitForElement(driver, By.Id("FirstName"));
            driver.FindElement(By.Id("FirstName")).SendKeys(formData.PersonalInfo.FirstName);
            driver.FindElement(By.Id("LastName")).SendKeys(formData.PersonalInfo.LastName);
            driver.FindElement(By.Id("DateOfBirth")).SendKeys(formData.PersonalInfo.DateOfBirth.ToString("yyyy-MM-dd"));
            driver.FindElement(By.CssSelector("form button[type='submit']")).Click();

            // Step 2
            WaitForElement(driver, By.Id("Email"));
            driver.FindElement(By.Id("Email")).SendKeys(formData.ContactInfo.Email);
            driver.FindElement(By.Id("Phone")).SendKeys(formData.ContactInfo.Phone);
            driver.FindElement(By.Id("Address")).SendKeys(formData.ContactInfo.Address);
            driver.FindElement(By.CssSelector("form button[type='submit']")).Click();

            // Step 3
            WaitForElement(driver, By.Id("Occupation"));
            driver.FindElement(By.Id("Occupation")).SendKeys(formData.ProfessionalInfo.Occupation);
            driver.FindElement(By.Id("Company")).SendKeys(formData.ProfessionalInfo.Company);
            driver.FindElement(By.Id("YearsOfExperience")).SendKeys(formData.ProfessionalInfo.YearsOfExperience.ToString());
            driver.FindElement(By.CssSelector("form button[type='submit']")).Click();
        }

    }
}
