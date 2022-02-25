using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Retailer.Backend.Bll.InvoiceService;
using Retailer.Backend.Core;
using Retailer.Backend.Core.AutoMapper;
using Retailer.Backend.Dal.InvoiceService;

namespace Retailer.Backend.InvoiceService
{
    public class Startup
    {
        private const string SERVICE_NAME = "Retailer.Backend.InvoiceService";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = SERVICE_NAME, Version = "v1" });
                c.CustomOperationIds(apiDescription => apiDescription.ActionDescriptor.RouteValues["action"]);
            });
            services.AddInvoiceDbContext(Configuration);
            services.AddCoreServices();
            services.AddAutoMapper(typeof(RetailerDataMappingProfile));
            services.AddInvoiceServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/InvoiceService.yaml", $"{SERVICE_NAME} v1");
                    c.DisplayOperationId();
                });
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
