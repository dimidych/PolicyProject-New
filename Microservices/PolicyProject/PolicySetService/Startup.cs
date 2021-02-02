using BaseDbContext;
using DevelopmentLogger;
using GroupService;
using LoginService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PolicyService.Models;
using PolicySetService.Models;

namespace PolicySetService
{
    public class Startup
    {
        private const string ServiceName = "PolicySet";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var policySetDbConnStr = Configuration.GetConnectionString($"{ServiceName}DbConnection");
            services.AddDbContext<IGroupDbContext, PolicySetDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, policySetDbConnStr));
            services.AddDbContext<ILoginDbContext, PolicySetDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, policySetDbConnStr));
            services.AddDbContext<IPolicyDbContext, PolicySetDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, policySetDbConnStr));
            services.AddDbContext<IPolicySetDbContext, PolicySetDbContext>(options =>
                BaseDbContextOptions.CreateDbContextOptionsAction(options, policySetDbConnStr));
            services.AddScoped(typeof(IGroupRepository), typeof(GroupRepository));
            services.AddScoped(typeof(ILoginRepository), typeof(LoginRepository));
            services.AddScoped(typeof(IPolicyRepository), typeof(PolicyRepository));
            services.AddScoped(typeof(IPolicySetRepository), typeof(PolicySetRepository));
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
