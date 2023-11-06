using Microsoft.AspNetCore.Mvc;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Account";
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public IActionResult Signup()
        {
            ViewBag.Title = "Sign up";
            return View();
        }
    }
}
