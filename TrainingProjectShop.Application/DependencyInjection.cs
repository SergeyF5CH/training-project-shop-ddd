using Microsoft.Extensions.DependencyInjection;
using TrainingProjectShop.Application.Customers;
using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Application.Products;

namespace TrainingProjectShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<OrderService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<ProductService>();

            return services;
        }
    }
}
