
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Retailer.Backend.Bll.StateMachines;
using Retailer.Backend.Core.Abstractions;

namespace Retailer.Backend.Bll.OrderService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<OrderEntityStateMachineFactory>();
            services.AddScoped<IDbOrderService, Services.OrderService>();
            return services;
        }
    }
}
