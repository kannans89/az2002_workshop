using _03ProductAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace _03ProductAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                           new Product { Id = 1, Name = "Product 1", Price = 100, IsAvailable = true },
                           new Product { Id = 2, Name = "Product 2", Price = 200, IsAvailable = true },
                          new Product { Id = 3, Name = "Product 3", Price = 300, IsAvailable = false }
                                                                    );

        }
    }
  
}
