using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAppService.AuthenticationManagers;
using UserAppService.InputModel;
using UserAppService.Models;
using UserAppService.OutputModels;

namespace UserAppService.Service.UserService
{
    public interface IUserService
    {
        BoolMethodResult RegisterUser(UserRegisterInputModel inputModel);

        void ForgotPassword(ForgotPasswordInputModel inputModel);

        BoolMethodResult ConfirmEmailRequest(ConfirmEmailRequestInputModel inputModel);

        BoolMethodResult ResetPasswordRequest(ResetUserPasswordRequestInputModel inputModel);

        BoolMethodResult ResetPassword(ResetUserPasswordInputModel inputModel);

        Task<User> VerifyUserByEmail(string email);

        User GetUserByEmail(string email);

        User GetUserByAlternateId(string alternateId);

        BoolMethodResult ConfirmEmail(ConfirmEmailInputModel inputModel);

        List<User> GetAllUsers();

        User GetUserByPhoneNumber(GetUserByPhoneNumberInputModel inputModel);

        User GetUserByUserName(GetUserByUserNameInputModel inputModel);
    }
}
