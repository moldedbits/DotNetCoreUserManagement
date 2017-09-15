using System.Collections.Generic;
using UserAppService.AutoMapper;
using UserAppService.Context;
using UserAppService.Dto;
using UserAppService.Models;
using Microsoft.AspNetCore.Identity;
using UserAppService.OutputModels;
using UserAppService.InputModel;
using UserAppService.Service.UserLoginAppServiceLayer;

namespace UserAppService.Service
{
    public class AuthService : IAuthService
    {
        #region CTORs

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IApplicationRoleService _appRoleService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserLoginService _userLoginService;

        public AuthService(ApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<User> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _appRoleService = new ApplicationRoleService(applicationDbContext, roleManager);
            _userLoginService = new UserLoginService(applicationDbContext, userManager, signInManager);
            AutoMapperConfiguration.Configure();
        }

        #endregion

        #region Application Roles Get Methods Passthrough
        
        public List<RoleDto> GetApplicationRoles()
        {
            return _appRoleService.GetApplicationRoles();
        }

        #endregion

        #region External Login Passthrough Methods

        public GoogleExternalLoginResult GoogleLogin(GoogleLoginInputModel inputModel)
        {
            return _userLoginService.GoogleLogin(inputModel);
        }

        #endregion
    }
}