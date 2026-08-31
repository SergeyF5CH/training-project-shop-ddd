using TrainingProjectShop.Application.Customers;
using TrainingProjectShop.Domain.Customers;

namespace TrainingProjectShop.Application.Tests.Customers
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task CreateCustomerAsync_ShouldCreateCustomer()
        {
            var repository = new FakeCustomerRepository();
            var service = new CustomerService(repository);

            var customerId = await service.CreateCustomerAsync(
                "Pedro",
                "pedro@email.com");

            Assert.NotEqual(Guid.Empty, customerId );
            Assert.NotNull(repository.Customer);
            Assert.Equal(customerId, repository.Customer!.Id );
            Assert.Equal("Pedro", repository.Customer.Name );
        }

        private class FakeCustomerRepository : ICustomerRepository
        {
            public Customer? Customer { get; private set; }

            public Task AddAsync(Customer customer)
            {
                Customer = customer;
                return Task.CompletedTask;
            }

            public Task<Customer?> GetByIdAsync(Guid id)
            {
                return Task.FromResult(Customer);
            }
        }
    }
}
