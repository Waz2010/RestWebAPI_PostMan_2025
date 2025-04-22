using Microsoft.AspNetCore.Mvc;

namespace ApplicationProfileUI.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
