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
                if (!HttpContext.Session.GetInt32("UserId").HasValue)
                {
                    TempData["Message"] = "Vous devez être connecté pour ajouter un produit à votre panier.";
                    return RedirectToAction("Login", "Account");
                }

                var basketId = HttpContext.Session.GetInt32("BasketId");
                if (basketId == null)
                {
                    Basket newBasket = new Basket()
                    {
                        UserId = (int)HttpContext.Session.GetInt32("UserId"),
                        TotalPrice = 0,
                        Active = true,
                        SellDate = null,
                        BasketProduct = new List<BasketProduct>()
                    };
                    db.Add(newBasket);
                    db.SaveChanges();

                    HttpContext.Session.SetInt32("BasketId", newBasket.BasketId);
                }
                Basket sessionBasket = db.Basket.FirstOrDefault(b => b.BasketId == (int)HttpContext.Session.GetInt32("BasketId"));
                if (sessionBasket == null) 
                {
                    //Todo:erreur
                    return RedirectToAction("Index");
                }

                // On recupere la liste des produits du panier
                List<BasketProduct> listBasketProduct = db.BasketProduct.Where(b => b.BPBasketId == sessionBasket.BasketId).ToList();
                if (listBasketProduct.Count() == 0) // Si la liste est vide, on ajoute le produit
                {
                    sessionBasket.BasketProduct = new List<BasketProduct>();
                    BasketProduct basketProduct = new BasketProduct()
                    {
                        BPBasketId = sessionBasket.BasketId,
                        BPProductId = model.ProductId,
                        Product = db.Product.Find(model.ProductId),
                        Quantity = 1
                    };

                    sessionBasket.BasketProduct.Add(basketProduct);
                    db.SaveChanges();
                }
                else // Sinon on vérifie si le produit est déjà dans le panier
                {
                    if (sessionBasket.BasketProduct.FirstOrDefault(b => b.BPProductId == model.ProductId) == null) // Si le produit n'est pas dans le panier, on l'ajoute
                    {
                        BasketProduct basketProduct = new BasketProduct()
                        {
                            BPBasketId = sessionBasket.BasketId,
                            BPProductId = model.ProductId,
                            Product = db.Product.Find(model.ProductId),
                            Quantity = 1
                        };

                        sessionBasket.BasketProduct.Add(basketProduct);
                        db.SaveChanges();
                    }
                    else // Sinon on augmente la quantité
                    {
                        BasketProduct basketProduct = sessionBasket.BasketProduct.FirstOrDefault(b => b.BPProductId == model.ProductId);
                        basketProduct.Quantity++;
                        db.SaveChanges();
                    }
                    //Todo: message quantité
                }
                TempData["Message"] = "Produit ajouté au panier";
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
