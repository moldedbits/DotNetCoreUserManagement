using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UserAppService;
using UserAppService.CustomFilter;
using UserAppService.InputModel;
using UserAppService.InputModels;
using UserAppService.OutputModels;
using UserAppService.Service;
using UserAppService.Utility.Extensions;

namespace UserManagement.Web.Controllers
{
    [AutofacControllerConfiguration]
    public class LoginController : BaseAPIController
    {
        private readonly IAuthService _service;
        public LoginController(IAuthService service)
        {
            _service = service;
        }

        #region External Login Routes

        [Route("api/ExternalLogin/Google")]
        public IHttpActionResult PostExternalGoogleLogin(GoogleLoginInputModel inputModel)
        {
            return Ok(_service.GoogleLogin(inputModel));
        }

        [Authorize]
        [Route("api/ExternalLogin/Facebook")]
        public IHttpActionResult PostExternalFacebookLogin(FacebookLoginInputModel inputModel)
        {
            return Ok(_service.FacebookLogin(inputModel));
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetCurrentLogin")]
        public IHttpActionResult GetCurrentLogin()
        {
             //var uu = Thread.CurrentPrincipal.Identity.GetUserId();
            return Ok(this.GetCurrentUserName());
        }

        #endregion

        #region Authentication

        /// <summary>
        /// Sign out (aka log off) the current user
        /// </summary>
        [Authorize]
        [AllowAnonymous]
        [Route("api/LogOut")]
        [ModelExceptionFilter]
        public BoolMethodResult LogOut()
        {
            return _service.LogOut();
        }

        #endregion
    }
}