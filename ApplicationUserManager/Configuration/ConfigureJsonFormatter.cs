using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace UserAppService.Configuration
{
    public class ConfigureJsonFormatter
    {
        public static void SetJsonFormatterSerializerSettings(HttpConfiguration config)
        { 
            //// Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";

        }
    }
}
