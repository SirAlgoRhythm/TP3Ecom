using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoixMagicroquanteWebsite.Models;

namespace NoixMagicroquanteWebsite.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager _userManager;

        public AccountController(NoixMagicroquanteWebsiteContext context) : base(context) 
        {
            _userManager = new UserManager();
        }

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
        [ValidateAntiForgeryToken]
        public IActionResult Signup(User user, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (user.Password != ConfirmPassword)
                {
                    TempData["Message"] = "La création de votre compte a échouée";
                    ModelState.AddModelError("ConfirmPassword", "Le mot de passe et la confirmation ne correspondent pas.");
                    return View(user);
                }
                else if (db.User.FirstOrDefault(u => u.Email == user.Email) == null)
                {
                    string hashedPassword = _userManager.HashPassword(user, user.Password + _userManager.Salt);
                    user.Password = hashedPassword;

                    db.User.Add(user);
                    db.SaveChanges();

                    int IsAdmin;
                    switch (user.IsAdmin)
                    {
                        case true:
                            IsAdmin = 1;
                            break;
                        default:
                            IsAdmin = 0;
                            break;
                    }

                    // Stockage de l'ID de l'utilisateur connecté dans la session
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetInt32("IsAdmin", IsAdmin);

                    TempData["Message"] = "Votre compte a été créé avec succès !";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "La création de votre compte a échouée";
                    return View(user);
                }
            }
            else
            {
                TempData["Message"] = "La création de votre compte a échouée";
                if (ConfirmPassword.IsNullOrEmpty())
                {
                    ModelState.AddModelError("ConfirmPassword", "Champ requis !");
                    return View(user);
                }
                else if (user.Password != ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Le mot de passe et la confirmation ne correspondent pas.");
                    return View(user);
                }

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
