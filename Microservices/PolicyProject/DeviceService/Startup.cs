using System;
using DevelopmentLogger;
using DevicePlatformEntity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DeviceService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var deviceDbConnStr = Configuration.GetConnectionString("DeviceDbConnection");
            services.AddDbContext<IDevicePlatformDbContext, DeviceDbContext>(options =>
                options.UseSqlServer(deviceDbConnStr,
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    }));
            services.AddDbContext<IDeviceDbContext, DeviceDbContext>(options => options.UseSqlServer(deviceDbConnStr,
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                }));
            services.AddScoped(typeof(IDevicePlatformRepository), typeof(DevicePlatformRepository));
            services.AddScoped(typeof(IDeviceRepository), typeof(DeviceRepository));
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "StoredDates HTTP API",
                    Version = "v1",
                    Description = "StoredDates"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddProvider(new CustomDevelopmentLoggerProvider("DeviceService.log"));
                app.UseDeveloperExceptionPage();
                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoredDates API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
