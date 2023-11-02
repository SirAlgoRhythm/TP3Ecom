using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class Tax
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public decimal Rate { get; set; }
    }
}
