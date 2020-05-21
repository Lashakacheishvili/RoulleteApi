using Microsoft.AspNetCore.Http;
using RoulleteApi.Common;
using RoulleteApi.Core;
using RoulleteApi.Repository.Ef;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RoulleteApi.StartupHelper
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, RoulleteDbContext dbContext)
        {
            var userId = context.User.Identity.IsAuthenticated ? context.User.GetUserIdFromPrincipal() : string.Empty;
            var ipAddress = context.Connection.RemoteIpAddress;
            var ipAddressToString = ipAddress.ToString();

            var request = await FormatRequestAsync(context.Request);
            var log = new RequestLog(userId, ipAddressToString, request);

            await dbContext.RequestLogs.AddAsync(log);
            await dbContext.SaveChangesAsync();
            await _next(context);
        }

        private async Task<string> FormatRequestAsync(HttpRequest request)
        {
            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }
    }
}
