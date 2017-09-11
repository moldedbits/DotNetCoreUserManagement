using System.Web.Http;

namespace UserAppService.Configuration
{
    public class ConfigureWebApi
    {
        public static void ConfigureRoute(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional });
        }

        public static void ConfigureFormatter(HttpConfiguration config)
        {
            ConfigureJsonFormatter.SetJsonFormatterSerializerSettings(config);
        }

        public static void SetWebApiConfig(HttpConfiguration config)
        {
            ConfigureRoute(config);
            ConfigureFormatter(config);
        }
    }
}
