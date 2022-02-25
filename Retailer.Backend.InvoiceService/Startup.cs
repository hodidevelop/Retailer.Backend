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
        private const string API_VERSION = "v1";

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
                c.SwaggerDoc(API_VERSION, new OpenApiInfo { Title = SERVICE_NAME, Version = API_VERSION });
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
                    c.SwaggerEndpoint($"/swagger/{API_VERSION}/swagger.json", $"{SERVICE_NAME} {API_VERSION}");
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
