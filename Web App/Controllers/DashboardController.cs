using Microsoft.AspNetCore.Mvc;

namespace Web_App.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
