using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace UserAppService.CustomFilter
{
    public class CustomUrlFilter : AuthorizeAttribute
    {
        public string Key { get; set; }

        public CustomUrlFilter()
        {

        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var pair = actionContext.Request.Headers.Where(x => x.Key == "Key").FirstOrDefault();

            if(pair.Key == null)
            {
                throw new Exception(ResourceFiles.LocalizedText.NotAuthorizedSource);
            }
            else if(pair.Value.ToList()[0] != ConfigurationManager.AppSettings [ "AppToken" ] )
            {
                throw new Exception(ResourceFiles.LocalizedText.NotAuthorizedSource);
            }
        }
    }
}
