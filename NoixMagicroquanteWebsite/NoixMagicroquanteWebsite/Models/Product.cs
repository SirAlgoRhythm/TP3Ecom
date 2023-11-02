using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Name { get; set; }
        public double PurchasePrice { get; set; }
        public double SellingPrice { get; set; }
        public int UnitId { get; set; }
        public int TaxId { get; set; }
        public int Stock { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Image { get; set; }
    }
}
