using System.Collections.Generic;
using UserAppService.Dto;
using UserAppService.InputModel;
using UserAppService.OutputModels;

namespace UserAppService.Service
{
    public interface IAuthService
    {
        //bool AddApplicationRole(RoleViewModel model);

        //RoleDto GetApplicationRoleById(GetApplicationRoleByIdInputModel inputModel);

        //RoleDto GetApplicationRoleByName(GetApplicationRoleByNameInputModel inputModel);

        List<RoleDto> GetApplicationRoles();

        //BoolMethodResult AddApplicationRole(AddApplicationRoleInputModel inputModel);

        //BoolMethodResult UpdateApplicationRole(UpdateApplicationRoleInputModel inputModel);

        //BoolMethodResult DeleteApplicationRole(DeleteApplicationRoleInputModel inputModel);

        //ChallengeResult ExternalLogin(ExternalLoginInputModel model);

        //Task<BoolMethodResult> ExternalLoginCallback();

        //ChallengeResult LinkLogin(LinkLoginInputModel model);

        //Task<BoolMethodResult> LinkLoginCallback();

        //Task<BoolMethodResult> ExternalLoginConfirmation(ExternalLoginConfirmationInputModel model);

        //BoolMethodResult LogOut();

        //BoolMethodResult RegisterUser(UserRegisterInputModel inputModel);

        //void ForgotPassword(ForgotPasswordInputModel inputModel);

        //BoolMethodResult ConfirmEmailRequest(ConfirmEmailRequestInputModel inputModel);

        //BoolMethodResult ResetPasswordRequest(ResetUserPasswordRequestInputModel inputModel);

        //BoolMethodResult ResetPassword(ResetUserPasswordInputModel inputModel);

        //BoolMethodResult AddUserRole(AddUserRoleInputModel inputModel);

        //BoolMethodResult AddUserRoles(AddUserRolesInputModel inputModel);

        //BoolMethodResult DeleteUserRole(DeleteUserRoleInputModel inputModel);

        //BoolMethodResult DeleteUserRoles(DeleteUserRolesInputModel inputModel);

        //BoolMethodResult DeleteAllUserRoles(DeleteAllUserRolesInputModel inputModel);

        //IList<RoleDto> GetUserRolesByUserId(GetUserRolesByUserIdInputModel inputModel);

        //void SendAppUserManager();

        //Task<User> VerifyUserByEmail(string email);

        //User GetUserByAlternateId(string alternateId);

        //User GetUserByEmail(string email);

        //BoolMethodResult AuthenticateUser(AuthenticateUserInputModel inputModel);

        //BoolMethodResult ConfirmEmail(ConfirmEmailInputModel inputModel);

        //List<User> GetAllUsers();

        //User GetUserByPhoneNumber(GetUserByPhoneNumberInputModel inputModel);

        //User GetUserByUserName(GetUserByUserNameInputModel inputModel);

        //#region External Login

        GoogleExternalLoginResult GoogleLogin(GoogleLoginInputModel inputModel);

        //FacebookExternalLoginResult FacebookLogin(FacebookLoginInputModel inputModel);

        //#endregion
    }
}