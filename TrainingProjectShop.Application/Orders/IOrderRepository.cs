using TrainingProjectShop.Domain.Orders;

namespace TrainingProjectShop.Application.Orders
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
    }
}
