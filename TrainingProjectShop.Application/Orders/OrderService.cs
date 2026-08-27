using TrainingProjectShop.Domain.Orders;

namespace TrainingProjectShop.Application.Orders
{
    public class OrderService 
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> CreateOrderAsync(Guid customerId)
        {
            var order = new Order(
                Guid.NewGuid(),
                customerId);

            await _orderRepository.AddAsync(order);

            return order.Id;
        }
    }
}
