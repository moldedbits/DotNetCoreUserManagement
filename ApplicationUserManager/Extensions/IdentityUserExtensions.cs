using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace UserAppService.Utility.Extensions
{
    public static class IdentityUserExtensions
    {
        public static string GetCurrentUserName(this IPrincipal identity)
        {
            ClaimsIdentity c = (ClaimsIdentity) identity.Identity;
            string username = c.Claims.First().Value;
            return username;
        }

        public static int GetCurrentUserId(this IPrincipal identity)
        {
            ClaimsIdentity c = (ClaimsIdentity) identity.Identity;
            int id = Convert.ToInt32(c.Claims.First(x=>x.Type == ClaimTypes.NameIdentifier).Value);
            return id;
        }
    }
}