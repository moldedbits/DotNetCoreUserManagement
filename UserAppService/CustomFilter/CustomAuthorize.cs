using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Web;
using UserAppService.Context;
using UserAppService.Exceptions;
using UserAppService.Models;
using UserAppService.Utility.Extensions;

namespace UserAppService.CustomFilter
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private string[] _roles { get; set; }
        private string _action { get; set; }
        public CustomAuthorize(string[] role)
        {
            _roles = role;
            //RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole, int, UserRole>(ApplicationDbContext));
            //Action = action;
        }

        //public CustomAuthorize(string resource, string action)
        //{
        //    Resource = resource;
        //    Action = action;
        //}

        //public override void OnAuthorization()
        //{
        //    //Check your post authorization logic 
        //    var roles = RoleManager.Roles.Where(x => _roles.Contains(x.Name)).Select(x => x.Id).ToList();

        //    var userId = HttpContext.Current.GetOwinContext().Authentication.User.Identity.GetUserId<int>();

        //    var user = UserManager.FindById(userId);

        //    if (!user.Roles.Any(x => roles.Contains(x.RoleId)))
        //    {
        //        throw new UserFriendlyException(System.Net.HttpStatusCode.Forbidden.ToString(), ResourceFiles.LocalizedText.UnauthorizedUser);
        //    }

        //}


        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        ApplicationUserManager userManager = null;

        //        var ctx = HttpContext.Current;
        //        if (ctx.IsNotNull())
        //        {
        //            var owin = ctx.GetOwinContext();
        //            if (owin.IsNotNull())
        //            {
        //                userManager = owin.GetUserManager<ApplicationUserManager>();
        //            }
        //        }

        //        if (userManager.IsNull())
        //        {
        //            var userStore = new UserStore<User, ApplicationRole, int, UserLogin, UserRole, UserClaim>(new ApplicationDbContext());
        //            //this should only happen for testing
        //            userManager = new ApplicationUserManager(userStore);
        //        }

        //        return userManager;
        //    }
        //}
        //public ApplicationRoleManager RoleManager
        //{
        //    get
        //    {
        //        ApplicationRoleManager roleManager = null;

        //        var ctx = HttpContext.Current;
        //        if (ctx.IsNotNull())
        //        {
        //            var owin = ctx.GetOwinContext();
        //            if (owin.IsNotNull())
        //            {
        //                roleManager = owin.Get<ApplicationRoleManager>();
        //            }
        //        }

        //        if (roleManager.IsNull())
        //        {
        //            var roleStore = new RoleStore<ApplicationRole, int, UserRole>(new ApplicationDbContext());
        //            //this should only happen for testing
        //            roleManager = new ApplicationRoleManager(roleStore);
        //        }

        //        return roleManager;
        //    }
        //}
    }
}