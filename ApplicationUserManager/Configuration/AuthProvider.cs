using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using UserAppService.AuthenticationManagers;
using UserAppService.Context;
using UserAppService.Models;
using UserAppService.Utility.Extensions;

public class AuthProvider : OAuthAuthorizationServerProvider
{
    public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
        context.Validated();
    }

    public ApplicationUserManager UserManager
    {
        get
        {
            ApplicationUserManager userManager = null;

            var ctx = HttpContext.Current;
            if( ctx.IsNotNull() )
            {
                var owin = ctx.GetOwinContext();
                if( owin.IsNotNull() )
                {
                    userManager = owin.GetUserManager<ApplicationUserManager>();
                }
            }

            if( userManager.IsNull() )
            {
                var userStore = new UserStore<User, ApplicationRole, int, UserLogin, UserRole, UserClaim>(new ApplicationDbContext());
                //this should only happen for testing
                userManager = new ApplicationUserManager(userStore);
            }

            return userManager;
        }
    }

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {

        var user =  this.UserManager.FindByName(context.UserName);

        var properties = new Dictionary<string, string>()
        {
            { ClaimTypes.NameIdentifier, user.Id.ToString() },
            { ClaimTypes.Name, context.UserName }
        };

        var identity = new ClaimsIdentity(context.Options.AuthenticationType);
        properties.ToList().ForEach(c => identity.AddClaim(new Claim(c.Key, c.Value)));

        var ticket = new AuthenticationTicket(identity, new AuthenticationProperties(properties));

        context.Validated(ticket);

        context.Request.Context.Authentication.SignIn(identity);
    }
}