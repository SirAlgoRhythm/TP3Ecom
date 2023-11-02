using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class Unit
    {
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Name { get; set; }
    }
}
