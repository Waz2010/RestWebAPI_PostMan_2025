using ApplicationProfileUI.Models;
using ApplicationProfileUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApplicationProfileUI.Controllers
{
    public class AuthController : Controller
    {
        private string _appUrl; // Private variable to hold the API URL from configuration
        private readonly HttpClient _httpclient;  // HttpClient instance for making HTTP requests 
        private readonly ILogger<ApplicationProfileUIController> _logger;

        public AuthController(IOptions<AppSettings> settings, IHttpClientFactory httpClientFactory, ILogger<ApplicationProfileUIController> logger)
        {
            //_appUrl = settings.Value.AppUrl; // Read the AppUrl value from appsettings.json using IOptions
            _appUrl = "https://testrestapi.setrents.com/api/ApplicationProfile";
            _httpclient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Login()
        {
            return View();
        }
    }
}
