using Microsoft.AspNetCore.Mvc;
using NoixMagicroquanteWebsite.Models;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToBasket(Product model)
        {
            //Todo: vérifier la quantité disponible
            if (!ModelState.IsValid) 
            {
                var basketId = HttpContext.Session.GetInt32("BasketId");
                if (basketId == null)
                {
                    Basket newBasket = new Basket()
                    {
                        UserId = (int)HttpContext.Session.GetInt32("UserId"),
                        TotalPrice = 0,
                        Active = true,
                        SellDate = DateTime.Now,
                        BasketProduct = new List<BasketProduct>()
                    };
                    db.Add(newBasket);
                    db.SaveChanges();

                    HttpContext.Session.SetInt32("BasketId", newBasket.BasketId);
                }
                Basket sessionBasket = db.Basket.Find(HttpContext.Session.GetInt32("BasketId"));
                if (sessionBasket == null) 
                {
                    //Todo:erreur
                    return RedirectToAction("Index");
                }

                if (sessionBasket.BasketProduct == null)
                {
                    sessionBasket.BasketProduct = new List<BasketProduct>();
                    BasketProduct basketProduct = new BasketProduct()
                    {
                        BPBasketId = sessionBasket.BasketId,
                        BPProductId = model.ProductId,
                        Quantity = 1
                    };

                    sessionBasket.BasketProduct.Add(basketProduct);
                    db.SaveChanges();
                }
                else
                {
                    if (sessionBasket.BasketProduct.First(b => b.BPProductId == model.ProductId) == null) 
                    {
                        BasketProduct basketProduct = new BasketProduct()
                        {
                            BPBasketId = sessionBasket.BasketId,
                            BPProductId = model.ProductId,
                            Quantity = 1
                        };

                        sessionBasket.BasketProduct.Add(basketProduct);
                        db.SaveChanges();
                    }
                    //Todo: message quantité
                }
                return RedirectToAction("Index");
            }
            else
            {
                //todo: afficher une erreur
                return RedirectToAction("Index");
            }
        }
    }
}
