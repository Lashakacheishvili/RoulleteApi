using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RoulleteApi.Common;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoulleteApi.StartupHelper
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenHelper _tokenHelper;
        private readonly string _JwtKey;
        private readonly int _tokenExpiresAfterInMinutes;

        public AccessTokenMiddleware(RequestDelegate next, IConfiguration configuration, TokenHelper tokenHelper)
        {
            _next = next;
            _tokenHelper = tokenHelper;
            _JwtKey = configuration["Jwt:key"];
            _tokenExpiresAfterInMinutes = int.Parse(configuration["Jwt:ExpiresAfterInMinutes"]);
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                await _next(context);
            }
            else
            {
                var token = _tokenHelper.CreateToken(_JwtKey, DateTime.Now.AddMinutes(_tokenExpiresAfterInMinutes),
                  new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, context.User.GetUserIdFromPrincipal())
                  });

                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;
                    httpContext.Response.Headers.Add("X-Response-Access-Token", new[] { token });

                    return Task.CompletedTask;
                }, context);

                await _next(context);
            }
        }
    }
}
