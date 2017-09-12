using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UserAppService.Settings;
using Microsoft.Extensions.Options;

namespace UserAppService.Controllers
{
    [Produces("application/json")]
    [Route("api/Configuration")]
    public class ConfigurationController : Controller
    {
        private IConfiguration Configuration { get; set; }
        private AppSettings AppSettings { get; set; }
        private IConfigurationRoot ConfigRoot { get; set; }

        public ConfigurationController(IOptions<AppSettings> settings, IConfiguration configuration, IConfigurationRoot configRoot)
        {
            AppSettings = settings.Value;
            Configuration = configuration;
            ConfigRoot = configRoot;
        }

        [HttpGet("reloadconfig")]
        public ActionResult ReloadConfig()
        {
            ConfigRoot.Reload();

            // this should give the latest value from config
            var lastVal = Configuration.GetValue<string>("AppSettings:ApplicationName");

            return Ok(lastVal);
        }

        [HttpGet("appname")]
        public string AppName()
        {
            return AppSettings.ApplicationName;
        }

        [HttpGet("appname_key")]
        public string AppNameKey()
        {
            return Configuration.GetValue<string>("AppSettings:ApplicationName");
        }
    }
}