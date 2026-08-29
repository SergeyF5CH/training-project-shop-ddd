using Microsoft.EntityFrameworkCore;
using TrainingProjectShop.Domain.Customers;
using TrainingProjectShop.Domain.Orders;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Infrastructure.Database
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ShopDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
