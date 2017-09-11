using UserAppService.Context;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using UserManagement.Web;
using System.Threading;
using System.Globalization;
using System.Web;
using UserAppService.Configuration;

namespace UserManagement
{
    public class UserManagementWebApi : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            ConfigureCurrentUICulture.SetCurrentUICulture();
        }
    }
}
