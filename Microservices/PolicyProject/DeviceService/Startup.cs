using BaseDbContext;
using DevelopmentLogger;
using DevicePlatformEntity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DeviceService
{
    public class Startup
    {
        private const string ServiceName = "Device";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var deviceDbConnStr = Configuration.GetConnectionString($"{ServiceName}DbConnection");
            services.AddDbContext<IDevicePlatformDbContext, DeviceDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, deviceDbConnStr));
            services.AddDbContext<IDeviceDbContext, DeviceDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, deviceDbConnStr));
            services.AddScoped(typeof(IDevicePlatformRepository), typeof(DevicePlatformRepository));
            services.AddScoped(typeof(IDeviceRepository), typeof(DeviceRepository));
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{ServiceName} HTTP API",
                    Version = "v1",
                    Description = ServiceName
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddProvider(new CustomDevelopmentLoggerProvider($"{ServiceName}Service.log"));
                app.UseDeveloperExceptionPage();
                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{ServiceName} API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
