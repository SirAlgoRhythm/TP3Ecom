namespace NoixMagicroquanteWebsite.Models
{
    public class Basket
    {
        public int BasketId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public double TotalPrice { get; set; }
        public bool Actif { get; set; }
        public DateTime? SellDate { get; set; }
        public ICollection<BasketProduct>? BasketProduct { get; set; }
    }
}
