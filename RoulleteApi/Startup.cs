using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoulleteApi.Application;
using RoulleteApi.Common;
using RoulleteApi.Core;
using RoulleteApi.Repository.Ef;
using RoulleteApi.StartupHelper;

namespace RoulleteApi
{
    public class Startup
    {
        private readonly string _connectionString;
        private readonly string _jwtKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = configuration["ConnectionStrings:Default"];
            _jwtKey = configuration["Jwt:Key"];
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRoulleteDbContext(_connectionString);

            services.AddControllers()
                .AddInvalidModelstateHandler();

            services.AddNswagDocument();

            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<RoulleteDbContext>()
                .AddDefaultTokenProviders();

            services.AddCustomAuthentication(_jwtKey);
            services.AddtServicesAndRepositories();

            services.AddSignalR();
            services.AddHostedService<JackpotWatcher>();
            
            services.AddTransient<TokenHelper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGlobalExceptionHandling();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(configuration => configuration.Path = string.Empty);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.UseMiddleware<AccessTokenMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<JackpotHub>("/hubs/jackpot");
            });
        }
    }
}
