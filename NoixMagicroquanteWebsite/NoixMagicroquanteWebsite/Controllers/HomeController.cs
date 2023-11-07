using Microsoft.AspNetCore.Mvc;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(NoixMagicroquanteWebsiteContext context) : base(context) { }

        public IActionResult Index()
        {
            ViewBag.Title = "Noix MagiCroquantes - Accueil";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Noix MagiCroquantes - Contact";
            return View();
        }
    }
}
