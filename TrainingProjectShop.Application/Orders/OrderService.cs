using TrainingProjectShop.Application.Customers;
using TrainingProjectShop.Application.Products;
using TrainingProjectShop.Domain.Orders;

namespace TrainingProjectShop.Application.Orders
{
    public class OrderService 
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public async Task<Guid> CreateOrderAsync(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer is null)
            {
                throw new InvalidOperationException("Customer not found");
            }

            var order = new Order(
                Guid.NewGuid(),
                customerId);

            await _orderRepository.AddAsync(order);

            return order.Id;
        }

        public async Task AddItemAsync(
            Guid orderId,
            Guid productId,
            int quantity)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
            {
                throw new InvalidOperationException("Order not found");
            }

            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                throw new InvalidOperationException("Product not found");
            }

            order.AddItem(
                product.Id,
                product.Price,
                quantity);

            await _orderRepository.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task ConfirmAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
            {
                throw new InvalidOperationException("Order not found");
            }

            order.Confirm();

            await _orderRepository.SaveChangesAsync();
        }

        public async Task PayAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
            {
                throw new InvalidOperationException("Order not found");
            }

            order.Pay();

            await _orderRepository.SaveChangesAsync();
        }

        public async Task CancelAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order is null)
            {
                throw new InvalidOperationException("Order not found");
            }

            order.Cancel();

            await _orderRepository.SaveChangesAsync();
        }
    }
}
