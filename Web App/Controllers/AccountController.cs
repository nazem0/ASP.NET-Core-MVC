using Microsoft.AspNetCore.Mvc;
using Repository_Pattern;
using System.Threading.Tasks;
using ViewModel;
namespace Web_App.Controllers
{
    public class AccountController : Controller
    {
        private AccountManager AccountManager;
        public AccountController(AccountManager _AccountManager)
        {
            AccountManager = _AccountManager;
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
        public async Task<IActionResult> Register(RegisterationViewModel RegisterationData)
        {
            if (ModelState.IsValid)
            {
                await AccountManager.Register(RegisterationData);
                return Json(RegisterationData);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginData)
        {
            if (ModelState.IsValid)
            {
                await AccountManager.Login(LoginData);
            }
            return RedirectToAction("GetAll", "Product");
        }


        public RedirectResult Logout()
        {
            AccountManager.Logout();
            return Redirect(HttpContext.Request.Headers["Referer"]);

        }
    }
}
