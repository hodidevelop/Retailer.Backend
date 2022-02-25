
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Retailer.Backend.Bll.StateMachines;
using Retailer.Backend.Core.Abstractions;

using System;

namespace Retailer.Backend.Bll.OrderService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<OrderEntityStateMachineFactory>();
            services.AddScoped<IDbOrderService, Services.OrderService>();
            var invoiceServiceBaseUrl = configuration["ConnectedServices:InvoiceService:BaseUrl"]
                ?? throw new InvalidOperationException("InvoiceService:BaseUrl is not configured.");
            services.AddHttpClient<IInvoiceService, Services.InvoiceService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(invoiceServiceBaseUrl);
            });
            return services;
        }
    }
}
