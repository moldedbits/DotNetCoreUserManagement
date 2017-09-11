using System.Web.Http;
using Autofac.Integration.WebApi;
using UserManagement.Web.IoC;
using UserAppService.Configuration;

namespace UserManagement.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = TypeRegistrar.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ConfigureWebApi.SetWebApiConfig(config);
        }
    }
}
