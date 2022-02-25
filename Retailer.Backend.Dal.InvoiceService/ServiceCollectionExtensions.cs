using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Retailer.Backend.Core.Abstractions;

namespace Retailer.Backend.Dal.InvoiceService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInvoiceDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            RetailerDbContext.IsMigration = false;

            services.AddDbContext<IRetailerInvoiceDbContext, RetailerDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
