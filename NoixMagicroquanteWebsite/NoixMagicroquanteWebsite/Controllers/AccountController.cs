using Microsoft.AspNetCore.Mvc;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Noix MagiCroquantes - Compte";
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.Title = "Noix MagiCroquantes - Connexion";
            return View();
        }

        public IActionResult Signup()
        {
            ViewBag.Title = "Noix MagiCroquantes - Créer un compte";
            return View();
        }
    }
}
