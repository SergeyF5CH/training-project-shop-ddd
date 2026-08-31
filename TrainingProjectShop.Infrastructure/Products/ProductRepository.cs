using Microsoft.EntityFrameworkCore;
using TrainingProjectShop.Application.Products;
using TrainingProjectShop.Domain.Products;
using TrainingProjectShop.Infrastructure.Database;

namespace TrainingProjectShop.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _dbContext;

        public ProductRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
