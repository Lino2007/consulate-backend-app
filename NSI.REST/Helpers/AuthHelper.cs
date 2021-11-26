using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NSI.REST.Helpers
{
    public static class AuthHelper
    {
        public static string GetRequestEmail(HttpContext httpContext)
        {
            ClaimsIdentity identity = httpContext.User.Identity as ClaimsIdentity;
            return identity?.FindFirst("preferred_username").Value;
        }
    }
}
