﻿using Microsoft.AspNetCore.Identity;
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

        public async Task<IActionResult> CheckEmail(string email)
        {
            var user = await db.User.FirstOrDefaultAsync(u => u.Email == email);
            bool isEmailTaken = user != null;
            return Json(isEmailTaken);
        }

        public async Task<IActionResult> CheckEmailWithId(string email, int id)
        {
            var user = await db.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return Json(false);
            else if (user.UserId == id)
                return Json(false);
            else
                return Json(true);
        }

        public IActionResult GetCurrentUserId()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            return Json(UserId);
        }

        public async Task<IActionResult> GetAllUsers(int id)
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var users = await db.User.Where(u => u.UserId != 1 && u.UserId != id && u.UserId != UserId).ToListAsync();
            return Json(users);
        }

        public async Task<IActionResult> GetUserById(int id, bool? isAdmin)
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (isAdmin == null)
            {
                var users = await db.User.Where(u => u.UserId == id && u.UserId != 1 && u.UserId != UserId).ToListAsync();
                return Json(users);
            }
            else
            {
                var users = await db.User.Where(u => u.UserId == id && u.UserId != 1 && u.UserId != UserId && u.IsAdmin == isAdmin).ToListAsync();
                return Json(users);
            }
        }

        public async Task<IActionResult> GetUsersLikeString(string searchString, bool? isAdmin)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (!UserId.HasValue || UserId.Value == 0)
            {
                // Gérer l'absence d'un UserId valide, par exemple en renvoyant une réponse d'erreur ou vide
                return Json(new List<User>()); // Exemple : retourne une liste vide
            }

            // Préparation de la requête de base
            var query = db.User.AsQueryable();

            // Filtre par isAdmin si spécifié
            if (isAdmin.HasValue)
            {
                query = query.Where(u => u.IsAdmin == isAdmin.Value);
            }

            // Exclusion des utilisateurs spécifiques
            query = query.Where(u => u.UserId != 1 && u.UserId != UserId);

            // Filtre par searchString si spécifié
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(searchString) ||
                    u.LastName.ToLower().Contains(searchString) ||
                    u.UserName.ToLower().Contains(searchString) ||
                    u.Email.ToLower().Contains(searchString));
            }

            // Exécution de la requête
            var users = await query.ToListAsync();

            // Retourner les utilisateurs sous forme de JSON
            return Json(users);
        }

        public async Task<IActionResult> GetUsersByIsAdmin(bool? isAdmin)
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (isAdmin == null)
            {
                return await GetAllUsers(UserId);
            }

            // Recherchez les utilisateurs dont l'un des champs de type chaîne correspond à la chaîne de recherche
            var users = await db.User.Where(u => u.IsAdmin == isAdmin && u.UserId != 1 && u.UserId != UserId).ToListAsync();

            // Vous pouvez ensuite retourner la liste des utilisateurs sous forme de JSON
            return Json(users);
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Noix MagiCroquantes - Compte";
            var user = db.User.Find(HttpContext.Session.GetInt32("UserId"));
            return View(user);
        }

        public IActionResult Users()
        {
            ViewBag.Title = "Noix MagiCroquantes - Utilisateurs";
            var users = db.User.Where(u => u.UserId != 1).ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Récupération de l'utilisateur dans la base de données
                    var userInDb = db.User.AsNoTracking().First(u => u.UserId == user.UserId);

                    string hashedPassword = _userManager.HashPassword(userInDb, user.Password);
                    userInDb.FirstName = user.FirstName;
                    userInDb.LastName = user.LastName;
                    userInDb.UserName = user.UserName;
                    userInDb.Email = user.Email;
                    userInDb.IsAdmin = user.IsAdmin;
                    userInDb.Password = hashedPassword;

                    db.Update(userInDb);
                    db.SaveChanges();

                    TempData["Message"] = "L'utilisateur à été modifié avec succès !";
                    return Json(new { success = true, message = "L'utilisateur à été modifié avec succès !" });
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "La modification de l'utilisateur a échoué.";
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                TempData["Error"] = "La modification de l'utilisateur a échoué.";
                return Json(new { success = false, message = "La modification de l'utilisateur a échoué." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAccount(User user, string NewPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Récupération de l'utilisateur dans la base de données
                    var userInDb = db.User.AsNoTracking().First(u => u.UserId == user.UserId);

                    if (!CheckPassword(userInDb, user.Password))
                    {
                        return Json(new { success = false, message = "Vérifiez les informations." });
                    }

                    string hashedPassword = _userManager.HashPassword(userInDb, NewPassword);
                    userInDb.FirstName = user.FirstName;
                    userInDb.LastName = user.LastName;
                    userInDb.UserName = user.UserName;
                    userInDb.Email = user.Email;
                    userInDb.Password = hashedPassword;

                    db.Update(userInDb);
                    db.SaveChanges();

                    TempData["Message"] = "Votre compte à été modifié avec succès !";
                    return Json(new { success = true, message = "Votre compte a été modifié avec succès !" });
                } 
                catch (Exception ex)
                {
                    TempData["Error"] = "La modification de l'utilisateur a échoué.";
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                TempData["Error"] = "La modification de votre compte a échoué.";
                return Json(new { success = false, message = "La modification de votre compte a échoué." });
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
                    TempData["Error"] = "Erreur, vérifiez les informations que vous avez entré.";
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

                        var basket = db.Basket.FirstOrDefault(b => b.UserId == utilisateur.UserId && b.Active == true);
                        if (basket != null)
                        {
                            HttpContext.Session.SetInt32("BasketId", basket.BasketId);
                        }

                        TempData["Message"] = "Connexion réussie, bon retour parmis nous !";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Error"] = "Erreur, vérifiez les informations que vous avez entré.";
                        return View(model);
                    }
                }
            }
            else
            {
                TempData["Error"] = "La connexion à votre compte a échoué";
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
                    TempData["Error"] = "La création de votre compte a échoué";
                    ModelState.AddModelError("ConfirmPassword", "Le mot de passe et la confirmation ne correspondent pas.");
                    return View(user);
                }
                else if (db.User.FirstOrDefault(u => u.Email == user.Email) == null)
                {
                    string hashedPassword = _userManager.HashPassword(user, user.Password);
                    user.Password = hashedPassword;

                    db.User.Add(user);
                    db.SaveChanges();

                    if (!HttpContext.Session.GetInt32("UserId").HasValue)
                    {
                        int IsAdmin = _userManager.IsAdmin(user);

                        // Stockage de l'ID de l'utilisateur connecté dans la session
                        HttpContext.Session.SetInt32("UserId", user.UserId);
                        HttpContext.Session.SetInt32("IsAdmin", IsAdmin);
                    }

                    TempData["Message"] = "Votre compte a été créé avec succès !";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "La création de votre compte a échoué";
                    return View(user);
                }
            }
            else
            {
                TempData["Error"] = "La création de votre compte a échoué";
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
                else
                {
                    return View(user);
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Déconnexion réussie, à bientôt !";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(int UserId)
        {
            // Récupération de l'utilisateur correspondant à l'id
            var user = db.User.First(u => u.UserId == UserId);

            // Supression du panier actif s'il y en a un
            var basket = db.Basket.FirstOrDefault(b => b.UserId == user.UserId && b.Active == true);
            if (basket != null)
            {
                db.Basket.Remove(basket);
            }

            // Supression de l'utilisateur
            db.User.Remove(user);
            db.SaveChanges();

            TempData["Message"] = "L'utilisateur a été supprimé avec succès !";
            return RedirectToAction("Users", "Account");
        }
    }
}
