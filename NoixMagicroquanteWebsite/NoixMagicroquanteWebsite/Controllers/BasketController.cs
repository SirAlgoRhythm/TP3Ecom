﻿using Microsoft.AspNetCore.Mvc;
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
                var ListBasketViewModel = new List<BasketViewModel>();
                foreach (BasketProduct item in ListProducts)
                {
                    BasketViewModel nBVM = new BasketViewModel()
                    {
                        Id = item.BPProductId,
                        Image = item.Product.Image,
                        Name = item.Product.Name,
                        Quantity = item.Quantity,
                        Stock = item.Product.Stock,
                        Price = item.Product.SellingPrice
                    };
                    ListBasketViewModel.Add(nBVM);
                }
                return View(ListBasketViewModel);
            }
            else
            {
                var ListProducts = db.BasketProduct.Where(b => b.BPBasketId == basket.BasketId).ToList();
                var ListBasketViewModel = new List<BasketViewModel>();
                foreach (BasketProduct item in ListProducts)
                {
                    BasketViewModel nBVM = new BasketViewModel()
                    {
                        Id = item.BPProductId,
                        Image = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Image,
                        Name = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Name,
                        Quantity = item.Quantity,
                        Stock = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).Stock,
                        Price = db.Product.FirstOrDefault(p => p.ProductId == item.BPProductId).SellingPrice
                    };
                    ListBasketViewModel.Add(nBVM);
                }
                return View(ListBasketViewModel);
            }
        }
    }
}
