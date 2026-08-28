using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Application.Products
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> CreateProductAsync(
            string name,
            Price price,
            string? description)
        {
            var product = new Product(
                Guid.NewGuid(),
                name,
                price,
                description);

            await _productRepository.AddAsync(product);

            return product.Id;
        }

        public async Task PublishProductAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                throw new InvalidOperationException("Product not found");
            }

            product.Publish();
        }

        public async Task ArchiveProductAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
            {
                throw new InvalidOperationException("Product not found");
            }

            product.Archived();
        }
    }
}
