using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Bll.InvoiceService.StateMachines;

using System;

namespace Retailer.Backend.Bll.InvoiceService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInvoiceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<InvoiceEntityStateMachineFactory>();
            services.AddScoped<IDbInvoiceService, Services.InvoiceService>();
            var orderServiceBaseUrl = configuration["ConnectedServices:OrderService:BaseUrl"]
                ?? throw new InvalidOperationException("InvoiceService:BaseUrl is not configured.");
            services.AddHttpClient<IOrderService, Services.OrderService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(orderServiceBaseUrl);
            });
            return services;
        }
    }
}
