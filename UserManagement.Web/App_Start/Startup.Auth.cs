using Apache.Web.Areas.HelpPage;
using Owin;
using System.Web.Http;
using UserAppService.Configuration;

namespace UserManagement.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            ConfigureAuthentication.ConfigureAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}