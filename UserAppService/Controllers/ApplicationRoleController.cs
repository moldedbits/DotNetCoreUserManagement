using UserAppService.InputModel;
using UserAppService.CustomFilter;
using System.Collections.Generic;
using UserAppService.Context;
using System.Reflection;
using UserAppService.Dto;
using Microsoft.AspNetCore.Mvc;
using UserAppService.Service;
using Microsoft.AspNetCore.Authorization;

namespace UserAppService.Controllers
{
    [Produces("application/json")]
    public class ApplicationRolesController : BaseAPIController
    {
        private readonly IAuthService _service;
        public ApplicationRolesController(IAuthService service, ApplicationDbContext appDBContext)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        [AllowAnonymous]
        [Route("api/ApplicationRoles")]
        public List<RoleDto> GetApplicationRoles(GetApplicationRolesModel inputModel)
        {
            //inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.GetApplicationRoles();
        }

    }
}