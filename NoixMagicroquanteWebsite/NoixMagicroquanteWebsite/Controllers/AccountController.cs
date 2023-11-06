using Microsoft.AspNetCore.Mvc;
using NoixMagicroquanteWebsite.Models;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class AccountController : Controller
    {
        protected NoixMagicroquanteWebsiteContext db = new NoixMagicroquanteWebsiteContext();
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

        [HttpPost]
        public IActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                if (db.User.FirstOrDefault(u => u.Email == user.Email) == null)
                {
                    db.User.Add(user);
                    db.SaveChanges();

                    TempData["Message"] = "Votre compte a été créé avec succès !";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "La création de votre compte à échouée";
                    return View(user);
                }
            }
            else
            {
                TempData["Message"] = "La création de votre compte à échouée";
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // Afficher l'erreur dans la console
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            return View(user);
        }
    }
}
