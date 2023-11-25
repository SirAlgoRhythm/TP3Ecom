using Microsoft.AspNetCore.Mvc;
using NoixMagicroquanteWebsite.Models;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class BasketController : BaseController
    {
        public BasketController(NoixMagicroquanteWebsiteContext context) : base(context) { }

        public IActionResult Index()
        {
            ViewBag.Title = "Noix MagiCroquantes - Panier";

            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                TempData["Message"] = "Vous devez être connecté pour accéder à votre panier.";
                return RedirectToAction("Login", "Account");
            }

            var user = db.User.Find(HttpContext.Session.GetInt32("UserId"));
            var basket = db.Basket.FirstOrDefault(b => b.UserId == user.UserId && b.Active == true);
            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = user.UserId,
                    TotalPrice = 0,
                    Active = true
                };
                db.Basket.Add(basket);
                db.SaveChanges();

                HttpContext.Session.SetInt32("BasketId", basket.BasketId);

                var ListProducts = db.BasketProduct.Where(b => b.BPBasketId == basket.BasketId).ToList();
                BasketViewModel bvm = new BasketViewModel();
                bvm.checkoutPostModels = new List<CheckoutPostModel>();
                foreach (BasketProduct item in ListProducts)
                {
                    CheckoutPostModel cpm = new CheckoutPostModel()
                    { 
                        ProductId = item.BPProductId,
                        Name = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Name,
                        Image = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Image,
                        Quantity = item.Quantity,
                        Stock = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Stock,
                        Price = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).SellingPrice
                    };
                    bvm.checkoutPostModels.Add(cpm);
                }
                bvm.TotalPrice = basket.TotalPrice;
                return View(bvm);
            }
            else
            {
                var ListProducts = db.BasketProduct.Where(b => b.BPBasketId == basket.BasketId).ToList();
                BasketViewModel bvm = new BasketViewModel();
                bvm.checkoutPostModels = new List<CheckoutPostModel>();
                foreach (BasketProduct item in ListProducts)
                {
                    CheckoutPostModel cpm = new CheckoutPostModel()
                    {
                        ProductId = item.BPProductId,
                        Name = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Name,
                        Image = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Image,
                        Quantity = item.Quantity,
                        Stock = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Stock,
                        Price = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).SellingPrice
                    };
                    bvm.checkoutPostModels.Add(cpm);
                }
                bvm.TotalPrice = basket.TotalPrice;
                return View(bvm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutBasket(List<BasketViewModel> products)
        {
            if (ModelState.IsValid)
            {
                var basket = db.Basket.First(b => b.BasketId == (int)HttpContext.Session.GetInt32("BasketId"));
                basket.Active = false;
                basket.SellDate = DateTime.Now;
                db.SaveChanges();

                HttpContext.Session.Remove("BasketId");

                TempData["Message"] = "Paiment accepté, merci de votre achat !";
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                TempData["Message"] = "Erreur, votre commande a échoué.";
                return RedirectToAction("Index");
            }
        }
    }
}
