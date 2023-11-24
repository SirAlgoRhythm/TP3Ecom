namespace NoixMagicroquanteWebsite.Models
{
    public class BasketProduct
    {
        public int Id { get; set; }
        public int BPProductId { get; set; }
        public Product? Product { get; set; }
        public int BPBasketId { get; set; }
        public Basket? Basket { get; set; }
        public int Quantity { get; set; }
    }
}
