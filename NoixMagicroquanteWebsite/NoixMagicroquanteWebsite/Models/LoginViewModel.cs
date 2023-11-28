using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Un courriel est requis !")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Un mot de passe est requis !")]
        public string? Password { get; set; }
    }
}
