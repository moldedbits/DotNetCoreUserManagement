using Microsoft.AspNetCore.Mvc;
using System;
using UserAppService.Context;
using UserAppService.Exceptions;
using UserAppService.Models;
using UserAppService.Utility.Extensions;
using Microsoft.AspNetCore.Identity;

namespace UserAppService
{
    public class BaseAPIController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BaseAPIController()
        {
        }

        public BaseAPIController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
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
                    var result = UserManager.FindByNameAsync(userName);

                    if (result.IsNull())
                        throw new UserFriendlyException(System.Net.HttpStatusCode.InternalServerError.ToString(), "User not found, please authenticate");

                    _currentUser = result.Result;
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException(System.Net.HttpStatusCode.InternalServerError.ToString(), "User not found, please authenticate");
                }

                return _currentUser;
            }
        }

        #endregion

        private UserManager<User> _userManager;

        public UserManager<User> UserManager
        {
            get
            {
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