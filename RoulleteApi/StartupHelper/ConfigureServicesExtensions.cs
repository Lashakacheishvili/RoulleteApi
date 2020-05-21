using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using RoulleteApi.Application;
using RoulleteApi.Repository.Ef;
using RoulleteApi.Repository.Implementation;
using RoulleteApi.Repository.Interfaces;
using RoulleteApi.Services.Interfaces;
using System.Text;

namespace RoulleteApi.StartupHelper
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection AddtServicesAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJackpotRepository, JackpotRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJackpotService, JackpotService>();

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, string key)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                  .AddJwtBearer(x =>
                  {
                      x.RequireHttpsMetadata = false;
                      x.SaveToken = true;
                      x.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                          ValidateIssuer = false,
                          ValidateAudience = false,
                      };
                  });

            return services;
        }

        public static IServiceCollection AddNswagDocument(this IServiceCollection services)
        {
            services.AddSwaggerDocument(config =>
            {
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Format should be 'Bearer jwt'. replace jwt with valid token",
                    In = OpenApiSecurityApiKeyLocation.Header
                }));
            });

            return services;
        }

        public static IServiceCollection AddRoulleteDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RoulleteDbContext>(options =>
                 options.UseSqlServer(connectionString));

            return services;
        }

        public static IMvcBuilder AddInvalidModelstateHandler(this IMvcBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(setupAction =>
             {
                 setupAction.InvalidModelStateResponseFactory = context =>
                 {
                     var problemDetails = new ValidationProblemDetails(context.ModelState)
                     {
                         Type = "ModelValidationProblem",
                         Title = "One or more model validation errors occured",
                         Status = StatusCodes.Status422UnprocessableEntity,
                         Detail = "See the error property for details",
                         Instance = context.HttpContext.Request.Path
                     };

                     problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                     return new UnprocessableEntityObjectResult(problemDetails)
                     {
                         ContentTypes = { "application/problem+json" }
                     };
                 };
             });

            return builder;
        }

        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = 500;
                    if (exceptionHandlerFeature != null)
                    {
                        // If logging is need add it here
                    }
                });
            });

            return app;
        }
    }
}
