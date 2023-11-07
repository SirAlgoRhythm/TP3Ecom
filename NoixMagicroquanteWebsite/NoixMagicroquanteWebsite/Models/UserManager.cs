using Microsoft.AspNetCore.Identity;

namespace NoixMagicroquanteWebsite.Models
{
    public class UserManager
    {
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly string _salt;

        public UserManager()
        {
            _passwordHasher = new PasswordHasher<User>();
            _salt = "NoixMagicroquanteWebsite";
        }

        public string Salt
        {
            get { return _salt; }
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyPassword(User user, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }
    }
}
