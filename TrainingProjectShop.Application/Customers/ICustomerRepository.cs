using TrainingProjectShop.Domain.Customers;

namespace TrainingProjectShop.Application.Customers
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task<Customer?> GetByIdAsync(Guid id);
    }
}
