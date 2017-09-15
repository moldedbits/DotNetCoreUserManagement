using System.Threading.Tasks;
//using UserAppService.ExternalLogin;
using UserAppService.InputModel;
using UserAppService.OutputModels;

namespace UserAppService.Service.UserLoginAppServiceLayer
{
    public interface IUserLoginService
    {
        //        void GetApplicationUserManager(ApplicationUserManager userManager);

        //        ChallengeResult ExternalLogin(ExternalLoginInputModel model);

        //        ChallengeResult LinkLogin(LinkLoginInputModel model);

        //        Task<BoolMethodResult> LinkLoginCallback();

        //        Task<BoolMethodResult> ExternalLoginCallback();

        //        BoolMethodResult LogOff();

        //        Task<BoolMethodResult> ExternalLoginConfirmation(ExternalLoginConfirmationInputModel model);

        #region External Login

        GoogleExternalLoginResult GoogleLogin(GoogleLoginInputModel inputModel);

//        FacebookExternalLoginResult FacebookLogin(FacebookLoginInputModel inputModel);

        #endregion
    }
}
