using Microsoft.AspNetCore.Mvc;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Noix MagiCroquantes - Accueil";
            return View();
        }
    }
}
