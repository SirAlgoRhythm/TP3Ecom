using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class Tax
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Un nom est requis !")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Un taux est requis !")]
        public float Rate { get; set; }
    }
}
