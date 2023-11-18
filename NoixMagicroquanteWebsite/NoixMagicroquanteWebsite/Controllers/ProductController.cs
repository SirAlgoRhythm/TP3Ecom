using Microsoft.AspNetCore.Mvc;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(NoixMagicroquanteWebsiteContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Noix MagiCroquantes - Produits";
            var products = db.Product.ToList();
            return View(products);
        }
    }
}
