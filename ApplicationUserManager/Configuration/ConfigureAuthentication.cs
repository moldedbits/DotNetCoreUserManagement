using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using UserAppService.AuthenticationManagers;
using UserAppService.Context;
using UserAppService.Models;
using UserAppService.Utility.Extensions;

namespace UserAppService.Configuration
{
    public class ConfigureAuthentication
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            var cookieValidationInterval = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings.Get("CookieValidationInterval").SetValueIfNullOrEmpty("1"));

            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //AuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = new AuthProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User, int>(
                        validateInterval: TimeSpan.FromMinutes(cookieValidationInterval),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager), getUserIdCallback:
                        delegate (ClaimsIdentity identity)
                        {
                            try
                            {
                                return identity.GetUserId<int>();
                            }
                            catch( FormatException ex ) //
                            {
                                return -1;
                            }
                        }),
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
