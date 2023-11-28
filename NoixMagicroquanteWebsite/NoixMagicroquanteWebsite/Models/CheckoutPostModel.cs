namespace NoixMagicroquanteWebsite.Models
{
    public class CheckoutPostModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public int? Stock { get; set; }
        public double Price { get; set; }
    }
}
