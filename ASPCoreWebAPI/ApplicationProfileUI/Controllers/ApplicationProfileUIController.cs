using Newtonsoft.Json.Serialization;
using ApplicationProfileUI.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ApplicationProfileUI.Controllers
{
    public class ApplicationProfileUIController : Controller
    {
        private string _appUrl; // Private variable to hold the API URL from configuration
        private readonly HttpClient _httpclient;  // HttpClient instance for making HTTP requests 
        private readonly ILogger<ApplicationProfileUIController> _logger;

        public ApplicationProfileUIController(IOptions<AppSettings> settings, IHttpClientFactory httpClientFactory, ILogger<ApplicationProfileUIController> logger)
        {
            //_appUrl = settings.Value.AppUrl; // Read the AppUrl value from appsettings.json using IOptions
            _appUrl = "https://testrestapi.setrents.com/api/ApplicationProfile";
            _httpclient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<ApplicationProfile> applications = new List<ApplicationProfile>();

            HttpResponseMessage response = _httpclient.GetAsync(_appUrl).Result; // // Make a synchronous GET request to the API URL
            if (response.IsSuccessStatusCode)  // Check if the HTTP response was successful (e.g., 200 OK)
            {
                string results = response.Content.ReadAsStringAsync().Result; // // Read the response content (JSON) as a string and save them into string variable

                List<ApplicationProfile>? data = null; // // Declare a nullable list to hold deserialized JSON data

                try
                {
                    // Attempt to deserialize the JSON into a list of ApplicationProfile objects
                    data = JsonConvert.DeserializeObject<List<ApplicationProfile>>(results,
                            new JsonSerializerSettings // Uses CamelCaseNamingStrategy to map camelCase JSON properties to C# PascalCase properties
                            {
                                ContractResolver = new DefaultContractResolver
                                {
                                    NamingStrategy = new CamelCaseNamingStrategy()
                                }
                            });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Deserialization failed: " + ex.Message);
                }

                if (data != null) // Check if deserialization was successful
                {
                    applications = data;
                }
                else
                {
                    _logger.LogWarning("API call failed with status code: {StatusCode}", response.StatusCode);
                }
            }
            // Return the list of application profiles to the view for rendering
            return View(applications);
        }

        // GET: Show the form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Handle form submission
        [HttpPost]
        public IActionResult Create(ApplicationProfile application)
        {

            if (!ModelState.IsValid) // check for ModelState errors
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // Log errors or display on screen
                    Console.WriteLine(error.ErrorMessage); // Simple console log
                                                           // Or use TempData to show errors in the UI if needed
                                                           //TempData["ModelStateErrors"] = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                }
                return View(application); // Return view with validation messages
            }
            string Appdata = JsonConvert.SerializeObject(application);
            StringContent content = new StringContent(Appdata, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpclient.PostAsync(_appUrl, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_Message"] = "Your Application has been submitted for this position...";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Something went wrong while submitting your application.");
            return View(application);
        }


        // GET: Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpclient.GetAsync($"{_appUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var application = JsonConvert.DeserializeObject<ApplicationProfile>(data);
                return View(application);
            }

            return NotFound();
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationProfile application)
        {
            if (!ModelState.IsValid)
            {
                return View(application);
            }

            application.ModifiedOn = DateTime.Now;
            application.ModifiedBy = "SupperAdmin"; // or your actual logged-in user

            string AppData = JsonConvert.SerializeObject(application);
            StringContent content = new StringContent(AppData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpclient.PutAsync($"{_appUrl}/{application.Id}", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["update_Message"] = "Application updated successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Update failed. Try again.");
            return View(application);
        }


        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpclient.GetAsync($"{_appUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var application = JsonConvert.DeserializeObject<ApplicationProfile>(data);
                return View(application);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpclient.GetAsync($"{_appUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var application = JsonConvert.DeserializeObject<ApplicationProfile>(result);
                return View(application);
            }

            TempData["error_Message"] = "Application not found.";
            return RedirectToAction("Index");
        }

        // POST: Actually delete the application
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpclient.DeleteAsync($"{_appUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["delete_Message"] = "Application deleted successfully.";
            }
            else
            {
                TempData["error_Message"] = "Failed to delete the application.";
            }

            return RedirectToAction("Index");
        }


    }
}

