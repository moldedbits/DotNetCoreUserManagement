using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using UserAppService.AuthenticationManagers;
using UserAppService.Context;
using UserAppService.Exceptions;
using UserAppService.Models;
using UserAppService.Service;
using UserAppService.Utility.Extensions;

namespace UserAppService
{
    public class BaseAPIController : ApiController
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected IAuthService _authService;

        public IAuthService AuthService
        {
            get
            {
                return _authService;
            }
        }

        public BaseAPIController()
        {
        }

        public BaseAPIController(ApplicationDbContext applicationDbContext, IAuthService service)
        {
            _applicationDbContext = applicationDbContext;
            _authService = service;
        }

        #region Fields

        private User _currentUser;

        public User CurrentUser
        {
            get
            {
                if (this.IsInTestMode)
                {
                    return testUser;
                }

                if (_currentUser.IsNotNull())
                {
                    return _currentUser;
                }

                var userName = this.GetCurrentUserName();

                try
                {
                    var currentUser = UserManager.FindByName(userName);

                    if (currentUser.IsNull())
                        throw new UserFriendlyException(System.Net.HttpStatusCode.InternalServerError.ToString(), "User not found, please authenticate");

                    _currentUser = currentUser;
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException(System.Net.HttpStatusCode.InternalServerError.ToString(), "User not found, please authenticate");
                }

                return _currentUser;
            }
        }

        #endregion

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager.IsNotNull())
                {
                    return _userManager;
                }
                else
                {
                    var ctx = HttpContext.Current;
                    if (ctx.IsNotNull())
                    {
                        var owin = ctx.GetOwinContext();
                        if (owin.IsNotNull())
                        {
                            _userManager = owin.GetUserManager<ApplicationUserManager>();
                        }
                    }
                }

                if (_userManager.IsNull())
                {
                    var userStore = new UserStore<User, ApplicationRole, int, UserLogin, UserRole, UserClaim>(new ApplicationDbContext());
                    //this should only happen for testing
                    _userManager = new ApplicationUserManager(userStore);
                }

                return _userManager;
            }

            set { _userManager = value; }
        }


        #region Properties

        public bool IsInTestMode { get; set; }

        internal User testUser { get; set; }

        #endregion

        #region Methods

        protected string GetCurrentUserName()
        {
            if (IsInTestMode)
            {
                return testUser == null ? string.Empty : testUser.UserName;
            }

            return User.Identity.Name;
        }

        #endregion
    }
}