using Microsoft.EntityFrameworkCore;
using TrainingProjectShop.Application.Customers;
using TrainingProjectShop.Domain.Customers;
using TrainingProjectShop.Infrastructure.Database;

namespace TrainingProjectShop.Infrastructure.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShopDbContext _dbContext;

        public CustomerRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
