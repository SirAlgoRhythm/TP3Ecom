using Microsoft.AspNetCore.Mvc;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class FactureController : BaseController
    {
        public FactureController(NoixMagicroquanteWebsiteContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Factures()
        {
            ViewBag.Title = "Noix MagiCroquantes - Factures";

            var factures = db.Basket.Where(b => b.Active == false).ToList();
            var users = db.User.ToList();
            foreach (var facture in factures)
            {
                facture.User = users.Find(u => u.UserId == facture.UserId);
            }

            return View(factures);
        }
    }
}
