//using AutoMapper;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Web;
//using UserAppService.AutoMapper;
//using UserAppService.Context;
//using UserAppService.Dto;
//using UserAppService.InputModel;
//using UserAppService.Models;
//using UserAppService.OutputModels;
//using UserAppService.ExternalLogin;
//using UserAppService.Utility.Extensions;
//using System;
//using UserAppService.Service.UserRoleService;
//using UserAppService.Service.UserLoginAppServiceLayer;
//using UserAppService.Service.UserService;
//using UserAppService.InputModels;

//namespace UserAppService.Service
//{
//    public class AuthService : IAuthService
//    {
//        #region CTORs

//        private readonly ApplicationDbContext _applicationDbContext;

//        private readonly IApplicationRoleService _appRoleService;
//        private readonly IUserRoleService _userRoleService;
//        private readonly IUserLoginService _userLoginService;
//        private readonly IUserService _userService;

//        private ApplicationUserManager _userManager;
//        private IAuthenticationManager authenticationManager;

//        public AuthService(ApplicationDbContext applicationDbContext)
//        {
//            _applicationDbContext = applicationDbContext;
//            _appRoleService = new ApplicationRoleService(applicationDbContext, RoleManager);
//            _userRoleService = new UserRoleService.UserRoleService();
//            _userLoginService = new UserLoginService(applicationDbContext, UserManager, SignInManager);
//            _userService = new UserService.UserService(this.UserManager);

//            AutoMapperConfiguration.Configure();
//        }

//        #endregion

//        #region Props

//        public ApplicationUserManager UserManager
//        {
//            get
//            {
//                if( _userManager.IsNotNull() )
//                {
//                    return _userManager;
//                }
//                else
//                {
//                    var ctx = HttpContext.Current;
//                    if( ctx.IsNotNull() )
//                    {
//                        var owin = ctx.GetOwinContext();
//                        if( owin.IsNotNull() )
//                        {
//                            _userManager = owin.GetUserManager<ApplicationUserManager>();
//                        }
//                    }
//                }

//                if( _userManager.IsNull() )
//                {
//                    var userStore = new UserStore<User, ApplicationRole, int, UserLogin, UserRole, UserClaim>(new ApplicationDbContext());
//                    //this should only happen for testing
//                    _userManager = new ApplicationUserManager(userStore);
//                }

//                return _userManager;
//            }

//            set { _userManager = value; }
//        }

//        public ApplicationRoleManager RoleManager
//        {
//            get
//            {
//                ApplicationRoleManager roleManager = null;

//                var ctx = HttpContext.Current;
//                if (ctx.IsNotNull())
//                {
//                    var owin = ctx.GetOwinContext();
//                    if (owin.IsNotNull())
//                    {
//                        roleManager = owin.Get<ApplicationRoleManager>();
//                    }
//                }

//                if (roleManager.IsNull())
//                {
//                    var roleStore = new RoleStore<ApplicationRole, int, UserRole>(new ApplicationDbContext());
//                    //this should only happen for testing
//                    roleManager = new ApplicationRoleManager(roleStore);
//                }

//                return roleManager;
//            }
//        }

//        public IAuthenticationManager AuthenticationManager
//        {
//            get
//            {
//                authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
//                if (authenticationManager == null)
//                {
//                    authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
//                }
//                return authenticationManager;
//            }
//        }

//        #endregion

//        #region Application Role Passthrough Methods

//        public BoolMethodResult AddApplicationRole(AddApplicationRoleInputModel inputModel)
//        {
//            return _appRoleService.AddApplicationRole(inputModel);
//        }

//        public BoolMethodResult UpdateApplicationRole(UpdateApplicationRoleInputModel inputModel)
//        {
//            return _appRoleService.UpdateApplicationRole(inputModel);
//        }

//        public BoolMethodResult DeleteApplicationRole(DeleteApplicationRoleInputModel inputModel)
//        {
//            return _appRoleService.DeleteApplicationRole(inputModel);
//        }

//        #endregion

//        #region External Login Passthrough Methods

//        public GoogleExternalLoginResult GoogleLogin(GoogleLoginInputModel inputModel)
//        {
//            return _userLoginService.GoogleLogin(inputModel);
//        }

//        public FacebookExternalLoginResult FacebookLogin(FacebookLoginInputModel inputModel)
//        {
//            return _userLoginService.FacebookLogin(inputModel);
//        }

//        public ChallengeResult ExternalLogin(ExternalLoginInputModel inputModel)
//        {
//            return _userLoginService.ExternalLogin(inputModel);
//        }

//        public ChallengeResult LinkLogin(LinkLoginInputModel inputModel)
//        {
//            return _userLoginService.LinkLogin(inputModel);
//        }

//        public async Task<BoolMethodResult> LinkLoginCallback()
//        {
//            return await _userLoginService.LinkLoginCallback();
//        }

//        public async Task<BoolMethodResult> ExternalLoginCallback()
//        {
//            return await _userLoginService.ExternalLoginCallback();
//        }

//        public BoolMethodResult LogOut()
//        {
//            var outputModel = new BoolMethodResult();

//            try
//            {
//                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

//                outputModel.WasSuccessful = true;
//                outputModel.Status = "Sign out successful";
//            }
//            catch(Exception ex)
//            {
//                outputModel.Status = @"Error signing off: {ex.Message}" ;
//            }

//            return outputModel;
//        }

//        public async Task<BoolMethodResult> ExternalLoginConfirmation(ExternalLoginConfirmationInputModel inputModel)
//        {
//            return await _userLoginService.ExternalLoginConfirmation(inputModel);
//        }

//        #endregion

//        #region User Role Passthrough Methods

//        public BoolMethodResult AddUserRole(AddUserRoleInputModel inputModel)
//        {
//            return _userRoleService.AddUserRole(inputModel);
//        }

//        public BoolMethodResult AddUserRoles(AddUserRolesInputModel inputModel)
//        {
//            return _userRoleService.AddUserRoles(inputModel);
//        }

//        public BoolMethodResult DeleteUserRole(DeleteUserRoleInputModel inputModel)
//        {
//            return _userRoleService.DeleteUserRole(inputModel);
//        }

//        public BoolMethodResult DeleteUserRoles(DeleteUserRolesInputModel inputModel)
//        {
//            return _userRoleService.DeleteUserRoles(inputModel);
//        }

//        public BoolMethodResult DeleteAllUserRoles(DeleteAllUserRolesInputModel inputModel)
//        {
//            return _userRoleService.DeleteAllUserRoles(inputModel);
//        }

//        public IList<RoleDto> GetUserRolesByUserId(GetUserRolesByUserIdInputModel inputModel)
//        {
//            return Mapper.Map<IList<RoleDto>> (_userRoleService.GetUserRolesByUserId(inputModel));
//        }

//        #endregion

//        #region User Controller Methods

//        public BoolMethodResult RegisterUser(UserRegisterInputModel inputModel)
//        {
//            return _userService.RegisterUser(inputModel);
//        }

//        public void ForgotPassword(ForgotPasswordInputModel inputModel)
//        {
//            _userService.ForgotPassword(inputModel);
//        }

//        public BoolMethodResult ConfirmEmailRequest(ConfirmEmailRequestInputModel inputModel)
//        {
//            return _userService.ConfirmEmailRequest(inputModel);
//        }

//        public BoolMethodResult ConfirmEmail(ConfirmEmailInputModel inputModel)
//        {
//            return _userService.ConfirmEmail(inputModel);
//        }

//        public BoolMethodResult ResetPasswordRequest(ResetUserPasswordRequestInputModel inputModel)
//        {
//            return _userService.ResetPasswordRequest(inputModel);
//        }

//        public BoolMethodResult ResetPassword(ResetUserPasswordInputModel inputModel)
//        {
//            return _userService.ResetPassword(inputModel);
//        }

        

//        #endregion

//        #region Helper Methods Passthrough

//        #region Get Methods Passthrough

//        #region Application Roles Get Methods Passthrough

//        public List<RoleDto> GetApplicationRoles()
//        {
//            return _appRoleService.GetApplicationRoles();
//        }

//        public RoleDto GetApplicationRoleById(GetApplicationRoleByIdInputModel inputModel)
//        {
//            return _appRoleService.GetApplicationRoleById(inputModel);
//        }

//        public RoleDto GetApplicationRoleByName(GetApplicationRoleByNameInputModel inputModel)
//        {
//            return _appRoleService.GetApplicationRoleByName(inputModel);
//        }

//        #endregion

//        #region User Get Methods Passthrough

//        public Task<User> VerifyUserByEmail(string email)
//        {
//            return _userService.VerifyUserByEmail(email);
//        }

//        public User GetUserByEmail(string email)
//        {
//            return _userService.GetUserByEmail(email);
//        }

//        public User GetUserByAlternateId(string alternateId)
//        {
//            return _userService.GetUserByAlternateId(alternateId);
//        }

//        public List<User> GetAllUsers()
//        {
//            return _userService.GetAllUsers();
//        }

//        public User GetUserByPhoneNumber(GetUserByPhoneNumberInputModel inputModel)
//        {
//            return _userService.GetUserByPhoneNumber(inputModel);
//        }

//        public User GetUserByUserName(GetUserByUserNameInputModel inputModel)
//        {
//            return _userService.GetUserByUserName(inputModel);
//        }

//        #endregion

//        #endregion

//        public void SendAppUserManager()
//        {
//            _userRoleService.GetApplicationUserManager(UserManager);
//        }

//        #endregion

//        public BoolMethodResult AuthenticateUser(AuthenticateUserInputModel inputModel)
//        {
//            var outputModel = new BoolMethodResult();

//            var result = SignInManager.PasswordSignInAsync(userName: inputModel.UserName, password: inputModel.Password, isPersistent: true, shouldLockout: false);

//            switch (result.Result)
//            {
//                case SignInStatus.Success:
//                    outputModel.WasSuccessful = true;
//                    outputModel.Status = ResourceFiles.LocalizedText.UserAuthenticated;
//                    break;

//                case SignInStatus.LockedOut:
//                    outputModel.Status = ResourceFiles.LocalizedText.LockedOut;
//                    break;

//                case SignInStatus.RequiresVerification:
//                    outputModel.Status = ResourceFiles.LocalizedText.AwaitingEmailConfirmation;
//                    break;

//                case SignInStatus.Failure:
//                default:
//                    outputModel.Status = "Invalid Login Attempt";
//                    break;
//            }

//            return outputModel;
//        }

//        private ApplicationSignInManager _signInManager;

//        public ApplicationSignInManager SignInManager
//        {
//            get
//            {
//                if (_signInManager.IsNull())
//                {
//                    var ctx = HttpContext.Current;
//                    if (ctx.IsNotNull())
//                    {
//                        var owin = ctx.GetOwinContext();
//                        if (owin.IsNotNull())
//                        {
//                            _signInManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
//                        }
//                    }
//                    else
//                    {
//                        _signInManager = new ApplicationSignInManager(this.UserManager, this.AuthenticationManager);
//                    }
//                }


//                return _signInManager;
//            }

//            set { _signInManager = value; }
//        }
//    }
//}
