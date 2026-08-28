using TrainingProjectShop.Application.Customers;
using TrainingProjectShop.Domain.Customers;

namespace TrainingProjectShop.Infrastructure.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new List<Customer>();

        public Task AddAsync(Customer customer)
        {
            _customers.Add(customer);

            return Task.CompletedTask;
        }

        public Task<Customer?> GetByIdAsync(Guid id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);

            return Task.FromResult(customer);
        }
    }
}
