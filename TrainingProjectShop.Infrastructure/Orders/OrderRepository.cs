using Microsoft.EntityFrameworkCore;
using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Domain.Orders;
using TrainingProjectShop.Infrastructure.Database;

namespace TrainingProjectShop.Infrastructure.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopDbContext _dbContext;

        public OrderRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
