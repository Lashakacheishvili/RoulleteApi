using System.Linq;
using System.Security.Claims;

namespace RoulleteApi.Common
{
    public static class UserHelper
    {
        public static string GetUserIdFromPrincipal(this ClaimsPrincipal user) 
            => user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }
}
