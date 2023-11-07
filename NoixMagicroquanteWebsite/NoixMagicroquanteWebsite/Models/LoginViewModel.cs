using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Champ requis !")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Password { get; set; }
    }
}
