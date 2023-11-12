using Microsoft.AspNetCore.Mvc;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class BasketController : BaseController
    {
        public BasketController(NoixMagicroquanteWebsiteContext context) : base(context) { }

        public IActionResult Index()
        {
            ViewBag.Title = "Noix MagiCroquantes - Panier";
            int UserId = (int)HttpContext.Session.GetInt32("UserId");
            //var ListProducts = db.BasketProduct.Where(b => b.UserId == UserId).ToList();

            return View();
        }
    }
}
