namespace NoixMagicroquanteWebsite.Models
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public double Price { get; set;}
    }
}
