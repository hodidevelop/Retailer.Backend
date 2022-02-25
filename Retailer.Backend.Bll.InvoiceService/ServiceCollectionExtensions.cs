using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Bll.InvoiceService.StateMachines;

namespace Retailer.Backend.Bll.InvoiceService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInvoiceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<InvoiceEntityStateMachineFactory>();
            services.AddScoped<IDbInvoiceService, Services.InvoiceService>();
            return services;
        }
    }
}
