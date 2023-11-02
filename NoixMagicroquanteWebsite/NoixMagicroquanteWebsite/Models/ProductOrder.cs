namespace NoixMagicroquanteWebsite.Models
{
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
