using System.ComponentModel.DataAnnotations;
using System.Data;

namespace NoixMagicroquanteWebsite.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
