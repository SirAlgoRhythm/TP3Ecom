using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NoixMagicroquanteWebsite.Models
{
    public class BasketProduct
    {
        [Key]
        [ForeignKey("BPProductId")]
        public int BPProductId { get; set; }
        public Product? Product { get; set; }

        [Key]
        [ForeignKey("BPBasketId")]
        public int BPBasketId { get; set; }
        public Basket? Basket { get; set; }
        public int Quantity { get; set; }
    }
}
