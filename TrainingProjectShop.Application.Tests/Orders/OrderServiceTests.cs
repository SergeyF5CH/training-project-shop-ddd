using TrainingProjectShop.Application.Customers;
using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Application.Products;
using TrainingProjectShop.Domain.Customers;
using TrainingProjectShop.Domain.Orders;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Application.Tests.Orders
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrderAsync_ShouldCreateOrder()
        {
            var customerRepository = new FakeCustomerRepository();
            var orderRepository = new FakeOrderRepository();
            var productRepository = new FakeProductRepository();

            var customer = new Customer(
                Guid.NewGuid(),
                "Pedro",
                new Email("pedro@email.com"));

            await customerRepository.AddAsync(customer);

            var service = new OrderService(
                orderRepository,
                customerRepository,
                productRepository);

            var orderId = await service.CreateOrderAsync(customer.Id);

            Assert.NotEqual(Guid.Empty, orderId);
            Assert.NotNull(orderRepository.Order);
            Assert.Equal(orderId, orderRepository.Order!.Id);
            Assert.Equal(customer.Id, orderRepository.Order.CustomerId);
        }

        [Fact]
        public async Task AddItemAsync_ShouldAddItemToOrder()
        {
            var customerRepository = new FakeCustomerRepository();
            var orderRepository = new FakeOrderRepository();
            var productRepository = new FakeProductRepository();

            var customer = new Customer(
                Guid.NewGuid(),
                "Pedro",
                new Email("pedro@email.com"));

            await customerRepository.AddAsync(customer);

            var order = new Order(
                Guid.NewGuid(),
                customer.Id);

            await orderRepository.AddAsync(order);

            var product = new Product(
                Guid.NewGuid(),
                "Warhammer 40K",
                new Price(100, "USD"),
                "Intercessor");

            await productRepository.AddAsync(product);

            var service = new OrderService(
                orderRepository,
                customerRepository,
                productRepository);

            await service.AddItemAsync(
                order.Id,
                product.Id,
                2);

            Assert.Single(order.Items);

            var item = order.Items.First();

            Assert.Equal(product.Id, item.ProductId);
            Assert.Equal(100, item.Price.Amount);
            Assert.Equal("USD", item.Price.Currency);
            Assert.Equal(2, item.Quantity);
            Assert.Equal(200, item.GetTotal());

            Assert.True(orderRepository.SaveChangesCalled);
        }

        [Fact]
        public async Task AddItemAsync_ShouldThrow_WhenProductNotFound()
        {
            var customerRepository = new FakeCustomerRepository();
            var orderRepository = new FakeOrderRepository();
            var productRepository = new FakeProductRepository();

            var customer = new Customer(
                Guid.NewGuid(),
                "Pedro",
                new Email("pedro@email.com"));

            await customerRepository.AddAsync(customer);

            var order = new Order(
                Guid.NewGuid(),
                customer.Id);

            await orderRepository.AddAsync(order);

            var service = new OrderService(
                orderRepository,
                customerRepository,
                productRepository);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.AddItemAsync(
                    order.Id,
                    Guid.NewGuid(),
                    1));

            Assert.Equal("Product not found", exception.Message);
        }

        [Fact]
        public async Task AddItemAsync_ShouldThrow_WhenOrderNotFound()
        {
            var customerRepository = new FakeCustomerRepository();
            var orderRepository = new FakeOrderRepository();
            var productRepository = new FakeProductRepository();

            var product = new Product(
                Guid.NewGuid(),
                "Warhammer 40K",
                new Price(100, "USD"),
                "Intercessor");

            await productRepository.AddAsync(product);

            var service = new OrderService(
                orderRepository,
                customerRepository,
                productRepository);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.AddItemAsync(
                    Guid.NewGuid(),
                    product.Id,
                    1));

            Assert.Equal("Order not found", exception.Message);
        }

        private class FakeOrderRepository : IOrderRepository
        {
            public Order? Order { get; private set; }
            public bool SaveChangesCalled { get; private set; }

            public Task AddAsync(Order order)
            {
                Order = order;
                return Task.CompletedTask;
            }

            public Task<Order?> GetByIdAsync(Guid id)
            {
                return Task.FromResult(Order);
            }

            public Task SaveChangesAsync()
            {
                SaveChangesCalled = true;
                return Task.CompletedTask;
            }
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

        private class FakeProductRepository : IProductRepository
        {
            public Product? Product { get; private set; }

            public Task AddAsync(Product product)
            {
                Product = product;
                return Task.CompletedTask;
            }

            public Task<Product?> GetByIdAsync(Guid id)
            {
                return Task.FromResult(Product);
            }
        }
    }
}