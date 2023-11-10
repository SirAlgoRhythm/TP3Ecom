using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            PasswordVerificationResult result = _userManager.VerifyPassword(user, db.User.Where(u => u.UserId == user.UserId).Select(u => u.Password).First(), providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Noix MagiCroquantes - Compte";
            var user = db.User.Find(HttpContext.Session.GetInt32("UserId"));
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int UserId, string FirstName, string LastName, string UserName, string Email, string OldPassword, string Password, string ConfirmPassword)
        {
            if (ModelState.IsValid)
            {
                // Récupération de l'utilisateur dans la base de données
                var userInDb = db.User.AsNoTracking().First(u => u.UserId == UserId);

                if (!CheckPassword(userInDb, OldPassword))
                {
                    TempData["Error"] = "Erreur, vérifiez les informations que vous avez entré.";
                    return RedirectToAction("Index", "Account");
                }

                if (Password != ConfirmPassword)
                {
                    TempData["Error"] = "Erreur, le nouveau mot de passe et la confirmation ne correspondent pas.";
                    return RedirectToAction("Index", "Account");
                }

                // Vérifiez que l'e-mail n'est pas déjà utilisé par un autre utilisateur.
                var existingUserWithEmail = db.User.AsNoTracking().SingleOrDefault(u => u.Email == Email);
                if (existingUserWithEmail != null && existingUserWithEmail.UserId != userInDb.UserId)
                {
                    TempData["Error"] = "Cette adresse e-mail est déjà utilisée par un autre compte.";
                    return RedirectToAction("Index", "Account");
                }

                string hashedPassword = _userManager.HashPassword(userInDb, Password);

                userInDb.FirstName = FirstName;
                userInDb.LastName = LastName;
                userInDb.UserName = UserName;
                userInDb.Email = Email;
                userInDb.Password = hashedPassword;

                db.Update(userInDb);
                db.SaveChanges();

                TempData["Message"] = "Votre compte à été modifié avec succès !";
                return RedirectToAction("Index", "Account");
            }
            else
            {
                TempData["Error"] = "La modification de votre compte a échoué";

                if (OldPassword.IsNullOrEmpty())
                {
                    return RedirectToAction("Index", "Account");
                }
                else if (ConfirmPassword.IsNullOrEmpty())
                {
                    return RedirectToAction("Index", "Account");
                }
                else if (Password != ConfirmPassword)
                {
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        // Afficher l'erreur dans la console
                        Console.WriteLine(error.ErrorMessage);
                    }

                    return RedirectToAction("Index", "Account");
                }
            }
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
                TempData["Message"] = "La connexion à votre compte a échoué";
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
                    TempData["Message"] = "La création de votre compte a échoué";
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
                    TempData["Message"] = "La création de votre compte a échoué";
                    return View(user);
                }
            }
            else
            {
                TempData["Message"] = "La création de votre compte a échoué";
                if (ConfirmPassword.IsNullOrEmpty())
                {
                    ModelState.AddModelError("ConfirmPassword", "Un mot de passe est requis !");
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
