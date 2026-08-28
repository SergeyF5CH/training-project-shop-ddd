using TrainingProjectShop.Application.Products;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public Task AddAsync(Product product)
        {
            _products.Add(product);

            return Task.CompletedTask;
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);

            return Task.FromResult(product);
        }
    }
}
