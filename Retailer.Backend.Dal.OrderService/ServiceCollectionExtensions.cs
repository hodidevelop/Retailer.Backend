using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Retailer.Backend.Core.Abstractions;

namespace Retailer.Backend.Dal.OrderService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            RetailerDbContext.IsMigration = false;

            services.AddDbContext<IRetailerOrderDbContext, RetailerDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
