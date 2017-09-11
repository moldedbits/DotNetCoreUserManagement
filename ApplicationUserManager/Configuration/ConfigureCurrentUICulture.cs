

using System.Globalization;
using System.Threading;
using System.Web;

namespace UserAppService.Configuration
{
    public class ConfigureCurrentUICulture
    {
        public static void SetCurrentUICulture()
        {
            //Set language for response based on the Accept-Language header from the request
            HttpRequest req = HttpContext.Current.Request;
            var lang = (req.UserLanguages != null && req.UserLanguages.Length != 0) ? req.UserLanguages[0] : "en-US";

            CultureInfo culture = CultureInfo.GetCultureInfo(lang);

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
