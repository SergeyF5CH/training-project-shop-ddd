using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Domain.Orders;

namespace TrainingProjectShop.Infrastructure.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();
        public Task AddAsync(Order order)
        {
            _orders.Add(order);

            return Task.CompletedTask;
        }
    }
}
