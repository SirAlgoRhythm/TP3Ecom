using Microsoft.AspNetCore.Identity;

namespace NoixMagicroquanteWebsite.Models
{
    public class UserManager
    {
        private readonly PasswordHasher<User> _passwordHasher;

        public UserManager()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        public int IsAdmin(User user)
        {
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
            return IsAdmin;
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
