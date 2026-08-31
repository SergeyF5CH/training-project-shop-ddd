using Microsoft.EntityFrameworkCore;
using TrainingProjectShop.Domain.Customers;
using TrainingProjectShop.Domain.Orders;
using TrainingProjectShop.Domain.Products;
using TrainingProjectShop.Infrastructure.Database;
using TrainingProjectShop.Infrastructure.Orders;

namespace TrainingProjectShop.IntegrationTests.Orders
{
    public class OrderRepositoryTests
    {
        private readonly DbContextOptions<ShopDbContext> _options;

        public OrderRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseNpgsql(
                    "Host=localhost;Port=5432;Database=TrainingProjectShopTest;Username=postgres;Password=123P@ssword123ITSWERYIMPORTANT")
                .Options;
        }

        [Fact]
        public async Task AddAndGetOrder_ShouldLoadItems()
        {
            await using var dbContext = new ShopDbContext(_options);

            await dbContext.Database.MigrateAsync();

            var customer = new Customer(
                Guid.NewGuid(),
                "Order Test Customer",
                new Email("order@test.com"));

            await dbContext.Customers.AddAsync(customer);

            var order = new Order(
                Guid.NewGuid(),
                customer.Id);

            var product = new Product(
                Guid.NewGuid(),
                "Warhammer 40K",
                new Price(100, "USD"),
                "Intercessor");

            await dbContext.Products.AddAsync(product);

            order.AddItem(
                product.Id,
                product.Price,
                2);

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            await using var secondDbContext = new ShopDbContext(_options);

            var repository = new OrderRepository(secondDbContext);

            var result = await repository.GetByIdAsync(order.Id);

            Assert.NotNull(result);
            Assert.Equal(order.Id, result!.Id);
            Assert.Equal(customer.Id, result.CustomerId);

            Assert.Single(result.Items);

            var item = result.Items.First();

            Assert.Equal(product.Id, item.ProductId);
            Assert.Equal(100, item.Price.Amount);
            Assert.Equal("USD", item.Price.Currency);
            Assert.Equal(2, item.Quantity);
            Assert.Equal(200, item.GetTotal());
        }
    }
}