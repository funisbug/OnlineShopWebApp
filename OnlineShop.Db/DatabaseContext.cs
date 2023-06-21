using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Comparison> Comparisons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderData> OrdersData { get; set; }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        { 
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.Parse("03b0f5f5-1690-40ea-8434-f3a4262eec2a"), Name = "Кубик 3x3x3", Cost = 949, Description = "Test1", ImagePath = "/images/Products/cdd948e6-15cb-4d2f-81ae-af39e88189fc.jpg" },
                new Product { Id = Guid.Parse("3b9a2b9b-70d0-41f7-80be-f1d028a19ce7"), Name = "Кубик 2x2x2", Cost = 799, Description = "Test2", ImagePath = "/images/Products/c8d87138-9a03-4187-bb36-0bcb96d0ac28.jpg" },
                new Product { Id = Guid.Parse("528f8578-a586-4b7b-855e-85dc9aad4423"), Name = "Кубик 5x5x5", Cost = 1299, Description = "Test3", ImagePath = "/images/Products/090f4001-ea3e-4415-9b72-1c6b78321948.jpg" },
                new Product { Id = Guid.Parse("85520332-cc96-4f9e-9acf-ce583aa2954b"), Name = "Кубик 4x4x4", Cost = 1099, Description = "Test4", ImagePath = "/images/Products/fea210b6-3c04-4dbd-b900-b24cf9468a43.jpg" },
                new Product { Id = Guid.Parse("0d8762ae-47d3-45ef-9d89-14e96b674221"), Name = "Кубик Брелок", Cost = 499, Description = "Test5", ImagePath = "/images/Products/edfaf6f0-ab7f-40cf-8794-d0d5e2610c84.jpg" },
                new Product { Id = Guid.Parse("2305ac13-19d1-44e3-924d-4eefe68e8216"), Name = "Кубик Пирамида", Cost = 699, Description = "Test6", ImagePath = "/images/Products/9e373301-f3a7-45e6-b322-a452db369e6f.jpg" }
                );
        }
    }
}
