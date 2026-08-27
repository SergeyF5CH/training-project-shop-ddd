using Microsoft.Extensions.DependencyInjection;
using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Domain.Orders;

namespace TrainingProjectShop.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<OrderService>();

            return services;
        }
    }
}
