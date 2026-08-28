using TrainingProjectShop.Domain.Products;

namespace TrainingProjectShop.Application.Products
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Product?> GetByIdAsync(Guid id);
    }
}
