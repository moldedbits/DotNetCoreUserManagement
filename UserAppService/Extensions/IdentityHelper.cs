using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using UserAppService.Models;

namespace UserAppService.Extensions
{
    public static class IdentityHelper
    {
        public static string GetEmail(this IIdentity identity)
        {
            var claimIdent = identity as ClaimsIdentity;
            return claimIdent != null
                && claimIdent.HasClaim(c => c.Type == "Email")
                ? claimIdent.FindFirst("Email").Value
                : string.Empty;
        }

        public static string GetUserName(this IIdentity identity)
        {
            var claimIdent = identity as ClaimsIdentity;
            return claimIdent != null
                && claimIdent.HasClaim(c => c.Type == "UserName")
                ? claimIdent.FindFirst("UserName").Value
                : string.Empty;
        }
    }
}
