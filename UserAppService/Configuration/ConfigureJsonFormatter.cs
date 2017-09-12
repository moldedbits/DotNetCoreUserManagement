using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace UserAppService.Configuration
{
    public class ConfigureJsonFormatter
    {
        public static void SetJsonFormatterSerializerSettings(IServiceCollection services) => services.AddMvc()
          .AddJsonOptions(options =>
          {
              options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
          });
    }
}
