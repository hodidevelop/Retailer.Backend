using Microsoft.Extensions.DependencyInjection;

using Retailer.Backend.Core.Abstractions;
using Retailer.Backend.Core.AutoMapper;
using Retailer.Backend.Core.Services;

namespace Retailer.Backend.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<ITimeService, TimeService>();
            services.AddAutoMapper(typeof(RetailerDataMappingProfile));
            return services;
        }
    }
}
