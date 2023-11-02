using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public double PurchasePrice { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public double SellingPrice { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Champ requis !")]
        public string? Image { get; set; }
        public bool Edible { get; set; }
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }
        public int TaxId { get; set; }
        public Tax? Tax { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<BasketProduct>? BasketProduct { get; set;}
    }
}
