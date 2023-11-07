using Microsoft.AspNetCore.Identity;
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

        public bool CheckPassword(User user, string providedPassword)
        {
            string storedHashedPassword = user.Password; // Obtenez cela de votre base de données
            PasswordVerificationResult result = _userManager.VerifyPassword(user, storedHashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Récupération de l'utilisateur correspondant à l'adresse email
                var utilisateur = db.User.SingleOrDefault(u => u.Email == model.Email);

                if (utilisateur == null)
                {
                    TempData["Message"] = "Erreur, vérifiez les informations que vous avez entré.";
                    return View(model);
                }
                else
                {
                    bool verifPassword = CheckPassword(utilisateur, model.Password);
                    if (verifPassword)
                    {
                        int IsAdmin = _userManager.IsAdmin(utilisateur);

                        // Stockage de l'ID de l'utilisateur connecté dans la session
                        HttpContext.Session.SetInt32("UserId", utilisateur.UserId);
                        HttpContext.Session.SetInt32("IsAdmin", IsAdmin);

                        TempData["Message"] = "Connexion réussie, bon retour parmis nous !";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Message"] = "Erreur, vérifiez les informations que vous avez entré.";
                        return View(model);
                    }
                }
            }
            else
            {
                TempData["Message"] = "La connexion à votre compte a échouée";
                return View(model);
            }
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
                    string hashedPassword = _userManager.HashPassword(user, user.Password);
                    user.Password = hashedPassword;

                    db.User.Add(user);
                    db.SaveChanges();

                    int IsAdmin = _userManager.IsAdmin(user);

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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Déconnexion réussie, à bientôt !";

            return RedirectToAction("Index", "Home");
        }
    }
}
