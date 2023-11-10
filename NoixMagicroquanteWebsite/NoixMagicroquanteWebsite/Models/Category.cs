using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Un nom est requis !")]
        public string? Name { get; set; }
    }
}
