using Microsoft.EntityFrameworkCore;
using NoixMagicroquanteWebsite.Models;

namespace NoixMagicroquanteWebsite
{
    public class NoixMagicroquanteWebsiteContext : DbContext
    {
        public NoixMagicroquanteWebsiteContext(DbContextOptions<NoixMagicroquanteWebsiteContext> options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketProduct> BasketProduct { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Tax> Tax { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=True";
            string data_base_name = "NoixMagicroquanteWebsiteDb";

            dbContextOptionsBuilder.UseSqlServer($"{connection_string};Database={data_base_name}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FirstName = "admin", LastName = "admin", UserName = "admin", Email = "admin@noixmagiques.com", Password = "AQAAAAEAACcQAAAAECp0ROY8Ai0bxYY7vrNEc2AMzZ9riapPYF4eisyY2+wsXUFLUMYsjtDTO3xCV4lrlA==", IsAdmin = true }
            );

            modelBuilder.Entity<Tax>().HasData(
                new Tax { Id = 1, Name = "TPS + TVQ 5% + 9.975%", Rate = 14.975F },
                new Tax { Id = 2, Name = "No Taxes", Rate = 0F }
            );

            modelBuilder.Entity<Unit>().HasData(
                new Unit { UnitId = 1, Name = "Item" },
                new Unit { UnitId = 2, Name = "Bouteille" },
                new Unit { UnitId = 3, Name = "Barre" },
                new Unit { UnitId = 4, Name = "Unité" },
                new Unit { UnitId = 5, Name = "Unité (par kg)" },
                new Unit { UnitId = 6, Name = "Boite" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Détergent" },
                new Category { CategoryId = 2, Name = "Outil nettoyant" },
                new Category { CategoryId = 3, Name = "Papier" },
                new Category { CategoryId = 4, Name = "Biscuits" },
                new Category { CategoryId = 5, Name = "Breuvage" },
                new Category { CategoryId = 6, Name = "Chocolat" },
                new Category { CategoryId = 7, Name = "Fruit" },
                new Category { CategoryId = 8, Name = "Légume" },
                new Category { CategoryId = 9, Name = "Café" },
                new Category { CategoryId = 10, Name = "Outil construction" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "ComLet", PurchasePrice = 4, SellingPrice = 8, Stock = 8, Image = "", Edible = false, UnitId = 2, TaxId = 1, CategoryId = 1 },
                new Product { ProductId = 2, Name = "JaBlex", PurchasePrice = 7, SellingPrice = 14, Stock = 10, Image = "", Edible = false, UnitId = 2, TaxId = 1, CategoryId = 1 },
                new Product { ProductId = 3, Name = "Mr.Blet", PurchasePrice = 2, SellingPrice = 4, Stock = 3, Image = "", Edible = false, UnitId = 2, TaxId = 1, CategoryId = 1 },
                new Product { ProductId = 4, Name = "Pasmalivre", PurchasePrice = 3, SellingPrice = 6, Stock = 0, Image = "", Edible = false, UnitId = 2, TaxId = 1, CategoryId = 1 },
                new Product { ProductId = 5, Name = "Stablex", PurchasePrice = 5, SellingPrice = 10, Stock = 21, Image = "", Edible = false, UnitId = 2, TaxId = 1, CategoryId = 1 },
                new Product { ProductId = 6, Name = "Brosse", PurchasePrice = 3, SellingPrice = 6, Stock = 41, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 2 },
                new Product { ProductId = 7, Name = "Balai", PurchasePrice = 10, SellingPrice = 20, Stock = 21, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 2 },
                new Product { ProductId = 8, Name = "Serviette", PurchasePrice = 6, SellingPrice = 12, Stock = 30, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 2 },
                new Product { ProductId = 9, Name = "Cuve", PurchasePrice = 12, SellingPrice = 24, Stock = 8, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 2 },
                new Product { ProductId = 10, Name = "Mopette", PurchasePrice = 17, SellingPrice = 34, Stock = 12, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 2 },
                new Product { ProductId = 11, Name = "Mouchoirs", PurchasePrice = 5, SellingPrice = 10, Stock = 60, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 3 },
                new Product { ProductId = 12, Name = "Essuie-tout", PurchasePrice = 6, SellingPrice = 12, Stock = 20, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 3 },
                new Product { ProductId = 13, Name = "Papier toilette", PurchasePrice = 9, SellingPrice = 18, Stock = 128, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 3 },
                new Product { ProductId = 14, Name = "Ore-crisp", PurchasePrice = 6, SellingPrice = 12, Stock = 5, Image = "", Edible = true, UnitId = 6, TaxId = 2, CategoryId = 4 },
                new Product { ProductId = 15, Name = "Crispie-Soda", PurchasePrice = 3, SellingPrice = 6, Stock = 12, Image = "", Edible = true, UnitId = 6, TaxId = 2, CategoryId = 4 },
                new Product { ProductId = 16, Name = "Petit-beurrier", PurchasePrice = 5, SellingPrice = 10, Stock = 20, Image = "", Edible = true, UnitId = 6, TaxId = 2, CategoryId = 4 },
                new Product { ProductId = 17, Name = "Gotorade", PurchasePrice = 2, SellingPrice = 4, Stock = 30, Image = "", Edible = true, UnitId = 2, TaxId = 2, CategoryId = 5 },
                new Product { ProductId = 18, Name = "Lait", PurchasePrice = 5, SellingPrice = 10, Stock = 5, Image = "", Edible = true, UnitId = 2, TaxId = 2, CategoryId = 5 },
                new Product { ProductId = 19, Name = "Oranginol", PurchasePrice = 1, SellingPrice = 2, Stock = 15, Image = "", Edible = true, UnitId = 2, TaxId = 2, CategoryId = 5 },
                new Product { ProductId = 20, Name = "Wondermilk", PurchasePrice = 1, SellingPrice = 2, Stock = 24, Image = "", Edible = true, UnitId = 3, TaxId = 2, CategoryId = 6 },
                new Product { ProductId = 21, Name = "Aeriol", PurchasePrice = 1, SellingPrice = 2, Stock = 32, Image = "", Edible = true, UnitId = 3, TaxId = 2, CategoryId = 6 },
                new Product { ProductId = 22, Name = "Orange", PurchasePrice = 1, SellingPrice = 2, Stock = 30, Image = "", Edible = true, UnitId = 4, TaxId = 2, CategoryId = 7 },
                new Product { ProductId = 23, Name = "Pomme", PurchasePrice = 1, SellingPrice = 2, Stock = 20, Image = "", Edible = true, UnitId = 4, TaxId = 2, CategoryId = 7 },
                new Product { ProductId = 24, Name = "Banane", PurchasePrice = 2, SellingPrice = 4, Stock = 18, Image = "", Edible = true, UnitId = 4, TaxId = 2, CategoryId = 7 },
                new Product { ProductId = 25, Name = "Cantaloup", PurchasePrice = 5, SellingPrice = 10, Stock = 4, Image = "", Edible = true, UnitId = 4, TaxId = 2, CategoryId = 7 },
                new Product { ProductId = 26, Name = "Tomate", PurchasePrice = 2, SellingPrice = 4, Stock = 9, Image = "", Edible = true, UnitId = 4, TaxId = 2, CategoryId = 8 },
                new Product { ProductId = 27, Name = "Cocombre", PurchasePrice = 2, SellingPrice = 4, Stock = 12, Image = "", Edible = true, UnitId = 4, TaxId = 2, CategoryId = 8 },
                new Product { ProductId = 28, Name = "Café corsé", PurchasePrice = 10, SellingPrice = 20, Stock = 20, Image = "", Edible = true, UnitId = 5, TaxId = 2, CategoryId = 9 },
                new Product { ProductId = 29, Name = "Café velouté", PurchasePrice = 10, SellingPrice = 20, Stock = 80, Image = "", Edible = true, UnitId = 5, TaxId = 2, CategoryId = 9 },
                new Product { ProductId = 30, Name = "Café décaféiné", PurchasePrice = 11, SellingPrice = 22, Stock = 30, Image = "", Edible = true, UnitId = 5, TaxId = 2, CategoryId = 9 },
                new Product { ProductId = 31, Name = "Tournevis", PurchasePrice = 3, SellingPrice = 6, Stock = 17, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 32, Name = "Scie ronde", PurchasePrice = 90, SellingPrice = 180, Stock = 9, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 33, Name = "Marteau", PurchasePrice = 15, SellingPrice = 30, Stock = 27, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 34, Name = "Équerre", PurchasePrice = 10, SellingPrice = 20, Stock = 18, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 35, Name = "Ruban à mesurer", PurchasePrice = 12, SellingPrice = 24, Stock = 13, Image = "", Edible = false, UnitId = 1, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 36, Name = "Clous 1 pouce", PurchasePrice = 4, SellingPrice = 8, Stock = 8, Image = "", Edible = false, UnitId = 6, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 37, Name = "Clous 2 pouces", PurchasePrice = 6, SellingPrice = 12, Stock = 6, Image = "", Edible = false, UnitId = 6, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 38, Name = "Clous 3 pouces", PurchasePrice = 8, SellingPrice = 16, Stock = 12, Image = "", Edible = false, UnitId = 6, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 39, Name = "Clous à béton", PurchasePrice = 10, SellingPrice = 20, Stock = 7, Image = "", Edible = false, UnitId = 6, TaxId = 1, CategoryId = 10 },
                new Product { ProductId = 40, Name = "Clous à finition", PurchasePrice = 6, SellingPrice = 12, Stock = 14, Image = "", Edible = false, UnitId = 6, TaxId = 1, CategoryId = 10 }
            );
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Unit)
                .WithMany()
                .HasForeignKey(p => p.UnitId);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Tax)
                .WithMany()
                .HasForeignKey(p => p.TaxId);
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Basket>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BasketProduct>()
                .HasKey( bp => new { bp.BPProductId, bp.BPBasketId });
            modelBuilder.Entity<BasketProduct>()
                .HasOne(bp => bp.Basket)
                .WithMany(p => p.BasketProduct)
                .HasForeignKey(bp => bp.BPBasketId);
            modelBuilder.Entity<BasketProduct>()
                .HasOne(bp => bp.Basket)
                .WithMany(p => p.BasketProduct)
                .HasForeignKey(bp => bp.BPProductId);
        }
    }
}
