using BaseDbContext;
using DevelopmentLogger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UserService.Models;

namespace UserService
{
    public class Startup
    {
        private const string ServiceName = "User";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var userDbConnStr = Configuration.GetConnectionString($"{ServiceName}DbConnection");
            services.AddDbContext<IUserDbContext, UserDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, userDbConnStr));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
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
