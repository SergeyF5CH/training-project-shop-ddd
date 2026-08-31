using Microsoft.EntityFrameworkCore;
using TrainingProjectShop.Domain.Customers;
using TrainingProjectShop.Infrastructure.Customers;
using TrainingProjectShop.Infrastructure.Database;

namespace TrainingProjectShop.IntegrationTests.Customers
{
    public class CustomerRepositoryTests
    {
        private readonly DbContextOptions<ShopDbContext> _options;

        public CustomerRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseNpgsql(
                    "Host=localhost;Port=5432;Database=TrainingProjectShopTest;Username=postgres;Password=123P@ssword123ITSWERYIMPORTANT")
                .Options;
        }

        [Fact]
        public async Task AddAndGetCustomer_ShouldWork()
        {
            await using var dbContext = new ShopDbContext(_options);

            await dbContext.Database.MigrateAsync();

            var repository = new CustomerRepository(dbContext);

            var customer = new Customer(
                Guid.NewGuid(),
                "Test Customer",
                new Email("test@test.com"));

            await repository.AddAsync(customer);

            var result = await repository.GetByIdAsync(customer.Id);

            Assert.NotNull(result);
            Assert.Equal(customer.Id, result!.Id);
            Assert.Equal("Test Customer", result.Name);
            Assert.Equal("test@test.com", result.Email.Value);
        }
    }
}