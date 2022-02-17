using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace go_blogs.Helper
{
    public static class DapatkanIdentitas
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                return user.Claims.FirstOrDefault(
                                          bebas =>
                                          bebas.Type == "Username"
                                          ) ?.Value ?? string.Empty;
            }
            return string.Empty;
        }
    }
}
