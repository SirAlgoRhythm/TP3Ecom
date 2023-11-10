using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Un prénom est requis !")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Un nom de famille est requis !")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Un nom d'utilisateur est requis !")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Un courriel est requis !")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Un mot de passe est requis !")]
        public string? Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
