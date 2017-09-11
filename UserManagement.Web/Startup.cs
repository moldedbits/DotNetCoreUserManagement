using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(UserManagement.Web.Startup))]
namespace UserManagement.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
