using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace UserAppService.CustomFilter
{
    public class CustomUrlFilter : AuthorizeAttribute
    {
        public string Key { get; set; }

        public CustomUrlFilter()
        {

        }

        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    var pair = actionContext.Request.Headers.Where(x => x.Key == "Key").FirstOrDefault();

        //    if(pair.Key == null)
        //    {
        //        throw new Exception(ResourceFiles.LocalizedText.NotAuthorizedSource);
        //    }
        //    else if(pair.Value.ToList()[0] != ConfigurationManager.AppSettings [ "AppToken" ] )
        //    {
        //        throw new Exception(ResourceFiles.LocalizedText.NotAuthorizedSource);
        //    }
        //}
    }
}
