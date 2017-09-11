using Autofac.Integration.WebApi;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using UserAppService;
using UserAppService.CustomFilter;
using UserAppService.InputModel;
using UserAppService.Models;
using UserAppService.OutputModels;
using UserAppService.Service;

namespace UserManagement.Web.Controllers
{
    [AutofacControllerConfiguration]
    //[ModelExceptionFilter]
    public class ApplicationUserController : BaseAPIController
    {
        private readonly IAuthService _service;
        public ApplicationUserController(IAuthService service)
        {
            _service = service;
        }

        [CustomUrlFilter]
        [Route("api/Register")]
        public BoolMethodResult RegisterUser(UserRegisterInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.RegisterUser(inputModel);
        }

        [Route("api/ForgotPassword")]
        public IHttpActionResult ForgotPassword(ForgotPasswordInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            _service.ForgotPassword(inputModel);
            return Ok();
        }

        [Route("api/ConfirmEmailLink/Request")]
        public BoolMethodResult ConfirmEmailRequest(ConfirmEmailRequestInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.ConfirmEmailRequest(inputModel);
        }

        [Route("api/ConfirmEmailLink")]
        public BoolMethodResult ConfirmEmail(ConfirmEmailInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.ConfirmEmail(inputModel);
        }

        [Route("api/PasswordReset/Request")]
        public BoolMethodResult PasswordResetRequest(ResetUserPasswordRequestInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.ResetPasswordRequest(inputModel);
        }

        [Route("api/PasswordReset")]
        public BoolMethodResult PasswordReset(ResetUserPasswordInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.ResetPassword(inputModel);
        }

        [Route("api/GetAllUsers")]
        public List<User> GetAllUsers(GetAllUsersInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.GetAllUsers();
        }

        [Route("api/GetUser/ByUserName")]
        public User GetUserByUserName(GetUserByUserNameInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.GetUserByUserName(inputModel);
        }

        [Route("api/GetUser/ByEmail")]
        public User GetUserByEmail(GetUserByEmailInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.GetUserByEmail(inputModel.Email);
        }
    }
}