using Microsoft.Extensions.DependencyInjection;
using TrainingProjectShop.Application.Customers;
using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Application.Products;
using TrainingProjectShop.Infrastructure.Customers;
using TrainingProjectShop.Infrastructure.Orders;
using TrainingProjectShop.Infrastructure.Products;

namespace TrainingProjectShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
