using BaseDbContext;
using DevelopmentLogger;
using DevicePlatformEntity;
using DeviceService;
using GroupService;
using LoginDevicesService.Models;
using LoginService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UserService.Models;

namespace LoginDevicesService
{
    public class Startup
    {
        private const string ServiceName = "LoginDevices";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var loginDevicesDbConnStr = Configuration.GetConnectionString($"{ServiceName}DbConnection");
            services.AddDbContext<IGroupDbContext, LoginDevicesDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, loginDevicesDbConnStr));
            services.AddDbContext<IUserDbContext, LoginDevicesDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, loginDevicesDbConnStr));
            services.AddDbContext<ILoginDbContext, LoginDevicesDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, loginDevicesDbConnStr));
            services.AddDbContext<IDevicePlatformDbContext, LoginDevicesDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, loginDevicesDbConnStr));
            services.AddDbContext<IDeviceDbContext, LoginDevicesDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, loginDevicesDbConnStr));
            services.AddDbContext<ILoginDevicesDbContext, LoginDevicesDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, loginDevicesDbConnStr));
            services.AddScoped(typeof(IGroupRepository), typeof(GroupRepository));
            services.AddScoped(typeof(ILoginRepository), typeof(LoginRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IDevicePlatformRepository), typeof(DevicePlatformRepository));
            services.AddScoped(typeof(IDeviceRepository), typeof(DeviceRepository));
            services.AddScoped(typeof(ILoginDevicesRepository), typeof(LoginDevicesRepository));
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
