using Microsoft.Extensions.DependencyInjection;
using TrainingProjectShop.Application.Orders;
using TrainingProjectShop.Infrastructure.Orders;

namespace TrainingProjectShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddSingleton<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
