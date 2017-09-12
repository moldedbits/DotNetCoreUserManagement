//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web;
//using UserAppService.Context;
//using UserAppService.ExternalLogin;
//using UserAppService.InputModel;
//using UserAppService.Models;
//using UserAppService.OutputModels;
//using UserAppService.Repositories;

//namespace UserAppService.Service.UserLoginAppServiceLayer
//{
//    public class UserLoginService : IUserLoginService
//    {
//        private ApplicationUserManager _userManager;
//        private IAuthenticationManager authenticationManager;
//        private ApplicationSignInManager _signInManager;
//        private readonly ApplicationDbContext _applicationDbContext;
//        private readonly IRepository<UserLogin> _userLogin;

//        public UserLoginService(ApplicationDbContext applicationDbContext, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
//        {
//            _applicationDbContext = applicationDbContext;
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _userLogin = new Repository<UserLogin>(_applicationDbContext);
//        }

//        public HttpRequestMessage CurrentRequest
//        {
//            get { return (HttpRequestMessage) HttpContext.Current.Items [ "MS_HttpRequestMessage" ]; }
//        }

//        public void GetApplicationUserManager(ApplicationUserManager userManager)
//        {
//            _userManager = userManager;
//        }

//        private async Task SignInAsync(User user, bool isPersistent)
//        {
//            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
//            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(_userManager));
//        }

//        public IAuthenticationManager AuthenticationManager
//        {
//            get
//            {
//                authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
//                if( authenticationManager == null )
//                {
//                    authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
//                }
//                return authenticationManager;
//            }
//        }

//        public GoogleExternalLoginResult GoogleLogin(GoogleLoginInputModel inputModel)
//        {
//            var outputModel = new GoogleExternalLoginResult() { Status = "External Login Failure", AuthTokenInfo = null};

//            string requestUri = string.Format(ResourceFiles.LocalizedText.GoogleRequestUri, inputModel.Token);
//            string tokenInfoMessage;

//            try
//            {
//                using (WebClient client = new WebClient())
//                {
//                    tokenInfoMessage = client.DownloadString(requestUri);
//                }
//                GoogleTokenInfo tokenInfo = JsonConvert.DeserializeObject<GoogleTokenInfo>(tokenInfoMessage);

//                var user = _userManager.FindByEmail(tokenInfo.Email);

//                if( user != null )
//                {
//                    _signInManager.SignIn(user, isPersistent: true, rememberBrowser: true);

//                    outputModel.WasSuccessful = true;
//                    outputModel.Status = ResourceFiles.LocalizedText.GmailLoginSuccessful;
//                    outputModel.AuthTokenInfo = tokenInfo;
//                }
//                else
//                {
//                    // If the user does not have an account, then prompt the user to create an account
//                    outputModel.Status = ResourceFiles.LocalizedText.UserAccountNotExists;
//                    //return result;
//                    if( inputModel.AutoRegister )
//                    {
//                        user = new User() { UserName = tokenInfo.Email, Email = tokenInfo.Email, CreatedById = -1, CreatedOn = DateTime.UtcNow, UpdatedById = -1, UpdatedOn = DateTime.UtcNow };

//                        var result = _userManager.Create(user);

//                        //Inserting data in UserLogin Table
//                        int userId;
//                        int.TryParse(tokenInfo.UserId, out userId);

//                        var userLogin = new UserLogin()
//                        {
//                            LoginProvider = inputModel.Provider,
//                            ProviderKey = inputModel.Token,
//                            UserId = userId
//                        };

//                        _userLogin.InsertOrUpdate(userLogin);
//                        _userLogin.Commit();

//                        if( !result.Succeeded )
//                        {
//                            throw new Exception(ResourceFiles.LocalizedText.UserRegisterFail + result.Errors.First());
//                        }

//                        // TODO: needs refactoring
//                        _signInManager.SignIn(user, isPersistent: true, rememberBrowser: true);

//                        outputModel.WasSuccessful = true;
//                        outputModel.Status = ResourceFiles.LocalizedText.GmailLoginSuccessful;
//                        outputModel.AuthTokenInfo = tokenInfo;
//                    }
//                }
//            }
//            catch(Exception ex)
//            {
//                outputModel.Status = $"Invalid Token: {ex.Message}";
//            }

//            return outputModel;
//        }

//        public FacebookExternalLoginResult FacebookLogin(FacebookLoginInputModel inputModel)
//        {
//            var outputModel = new FacebookExternalLoginResult() { Status = "External Login Failure", AuthTokenInfo = null };
//            var appToken = ConfigurationManager.AppSettings [ "FacebookAppToken" ];

//            string requestUri = string.Format(ResourceFiles.LocalizedText.FacebookRequestUri, inputModel.Token, appToken);
//            string tokenInfoMessage;

//            try
//            {
//                using( WebClient client = new WebClient() )
//                {
//                    tokenInfoMessage = client.DownloadString(requestUri);
//                }
//                FacebookTokenInfo tokenInfo = new FacebookTokenInfo();

//                dynamic jObj = (JObject) JsonConvert.DeserializeObject(tokenInfoMessage);
//                tokenInfo.UserId = jObj [ "data" ] [ "user_id" ];

//                var user = _userManager.FindByEmail(inputModel.Email);

//                if( user != null )
//                {
//                    _signInManager.SignIn(user, isPersistent: true, rememberBrowser: true);

//                    outputModel.WasSuccessful = true;
//                    outputModel.Status = ResourceFiles.LocalizedText.FacebookLoginSuccessful;
//                    outputModel.AuthTokenInfo = tokenInfo;
//                }
//                else
//                {
//                    outputModel.Status = ResourceFiles.LocalizedText.UserAccountNotExists;
//                    //return result;
//                    if( inputModel.AutoRegister )
//                    {
//                        user = new User() { UserName = inputModel.Email, Email = inputModel.Email, CreatedById = -1, CreatedOn = DateTime.UtcNow, UpdatedById = -1, UpdatedOn = DateTime.UtcNow };

//                        var result = _userManager.Create(user);

//                        //Inserting data in UserLogin Table
//                        int userId;
//                        int.TryParse(tokenInfo.UserId, out userId);

//                        var userLogin = new UserLogin()
//                        {
//                            LoginProvider = inputModel.Provider,
//                            ProviderKey = inputModel.Token,
//                            UserId = userId
//                        };

//                        _userLogin.InsertOrUpdate(userLogin);
//                        _userLogin.Commit();

//                        if( !result.Succeeded )
//                        {
//                            throw new Exception(ResourceFiles.LocalizedText.UserRegisterFail + result.Errors.First());
//                        }

//                        // TODO: needs refactoring
//                        _signInManager.SignIn(user, isPersistent: true, rememberBrowser: true);

//                        outputModel.WasSuccessful = true;
//                        outputModel.Status = ResourceFiles.LocalizedText.FacebookLoginSuccessful;
//                        outputModel.AuthTokenInfo = tokenInfo;
//                    }
//                }
//            }
//            catch( Exception ex )
//            {
//                outputModel.Status = $"Invalid Token: {ex.Message}";
//            }

//            return outputModel;
//        }

//        public ChallengeResult ExternalLogin(ExternalLoginInputModel model)
//        {
//            return new ChallengeResult(model.Provider, model.ReturnUrl, CurrentRequest);
//        }

//        public ChallengeResult LinkLogin(LinkLoginInputModel model)
//        {
//            // Request a redirect to the external login provider to link a login for the current user
//            return new ChallengeResult(model.Provider, "api/LinkLoginCallback", HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserId(), CurrentRequest);
//        }

//        public async Task<BoolMethodResult> LinkLoginCallback()
//        {
//            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(ChallengeResult.XsrfKey, HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserId());

//            var resultToLink = new BoolMethodResult { };

//            if( loginInfo == null )
//            {
//                resultToLink.WasSuccessful = false;
//                //Error
//            }
//            IdentityResult result = await this._userManager.AddLoginAsync(1, loginInfo.Login);
//            if( result.Succeeded )
//            {
//                resultToLink.WasSuccessful = true;
//            }
//            return resultToLink; // return boolResult base on success
//        }

//        public async Task<BoolMethodResult> ExternalLoginCallback()
//        {
//            var result = new BoolMethodResult { };
//            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
//            if( loginInfo == null )
//            {
//                result.WasSuccessful = false;
//                return result;
//            }

//            // Sign in the user with this external login provider if the user already has a login
//            var user = await this._userManager.FindAsync(loginInfo.Login);
//            if( user != null )
//            {
//                await SignInAsync(user, isPersistent: false);
//                result.WasSuccessful = true;
//                return result;
//            }
//            else
//            {
//                // If the user does not have an account, then prompt the user to create an account
//                result.WasSuccessful = false;
//                result.Status = "user does not have an account,prompt the user to create an account";
//                return result;
//            }
//        }

//        public BoolMethodResult LogOff()
//        {
//            AuthenticationManager.SignOut();
//            return new BoolMethodResult { WasSuccessful = true };
//        }

//        public async Task<BoolMethodResult> ExternalLoginConfirmation(ExternalLoginConfirmationInputModel model)
//        {
//            var om = new BoolMethodResult { WasSuccessful = false };
//            if( HttpContext.Current.GetOwinContext().Authentication.User.Identity.IsAuthenticated )
//            {
//                om.WasSuccessful = true;
//                return om;
//            }

//            //Validate first

//            // if(model.Validate())
//            {
//                // Get the information about the user from the external login provider
//                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
//                if( info == null )
//                {
//                    return new BoolMethodResult { WasSuccessful = false, Status = "ExternalLoginFailure" };
//                }

//                var user = new User() { UserName = model.Email, Email = model.Email };
//                IdentityResult result = await _userManager.CreateAsync(user);
//                if( result.Succeeded )
//                {
//                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
//                    if( result.Succeeded )
//                    {
//                        await SignInAsync(user, isPersistent: false);
//                        //Todo
//                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
//                        // Send an email with this link
//                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
//                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
//                        // SendEmail(user.Email, callbackUrl, "Confirm your account", "Please confirm your account by clicking this link");

//                        return new BoolMethodResult { WasSuccessful = false, Status = "ExternalLoginFailure" };
//                    }
//                }
//                //  AddErrors(result);
//            }

//            //// ViewBag.ReturnUrl = returnUrl;
//            return om;
//        }
//    }
//}
