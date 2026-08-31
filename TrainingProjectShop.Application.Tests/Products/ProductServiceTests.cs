using TrainingProjectShop.Application.Products;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Application.Tests.Products
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task CreateProductAsync_ShouldCreateProduct()
        {
            var repository = new FakeProductRepository();
            var service = new ProductService(repository);

            var price = new Price(50, "USD");

            var productId = await service.CreateProductAsync(
                "Warhammer 40K",
                price,
                "Intercessor");

            Assert.NotEqual(Guid.Empty, productId);
            Assert.NotNull(repository.Product);
            Assert.Equal(productId, repository.Product!.Id);
            Assert.Equal("Warhammer 40K", repository.Product.Name);
            Assert.Equal(price.Amount, repository.Product.Price.Amount);
            Assert.Equal(price.Currency, repository.Product.Price.Currency);
            Assert.Equal("Intercessor", repository.Product.Description);
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
