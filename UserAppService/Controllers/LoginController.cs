using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserAppService.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserAppService.Controllers
{
    [Produces("application/json")]
    public class LoginController : Controller
    {
        private UserManager<User> _userManager;
        //private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        string currentUserId;
        public LoginController(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
           // currentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
        [Authorize]
        [HttpPost]
        [Route("api/GetCurrentLogin")]
        public async Task<string> GetCurrentLoginAsync(InputModel.AnonymousInputModel model)
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);

            //var user1 = await _userManager.GetUserAsync(HttpContext.User);
            //string ii = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //return user.UserName;
            return string.Empty;
        }

        [Authorize]
        [HttpGet("claims")]
        public object Claims()
        {
            return User.Claims.Select(c =>
            new
            {
                Type = c.Type,
                Value = c.Value
            });
        }
    }
}