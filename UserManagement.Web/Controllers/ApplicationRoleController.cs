using Autofac.Integration.WebApi;
using System.Web.Http;
using UserAppService;
using UserAppService.InputModel;
using UserAppService.Service;
using UserAppService.CustomFilter;
using System.Collections.Generic;
using UserAppService.Context;
using System.Reflection;
using UserAppService.Dto;
using UserAppService.OutputModels;

namespace UserManagement.Web.Controllers
{
    [AutofacControllerConfiguration]
    [ModelExceptionFilter]
    [CustomAuthorize(new[] { "ADMIN" })]
    public class ApplicationRolesController : BaseAPIController
    {
        private readonly IAuthService _service;
        public ApplicationRolesController(IAuthService service, ApplicationDbContext appDBContext)
        {
            _service = service;
        }

        [Route("api/ApplicationRoles/Get")]
        public List<RoleDto> PostApplicationRoles(GetApplicationRolesModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.GetApplicationRoles();
        }

        [Route("api/ApplicationRoles/Add")]
        public BoolMethodResult PostAddApplicationRole(AddApplicationRoleInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.AddApplicationRole(inputModel);
        }

        [Route("api/ApplicationRoles/Update")]
        public BoolMethodResult PostUpdateApplicationRole(UpdateApplicationRoleInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.UpdateApplicationRole(inputModel);
        }

        [Route("api/ApplicationRoles/Delete")]
        public BoolMethodResult PostDeleteApplicationRole(DeleteApplicationRoleInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.DeleteApplicationRole(inputModel);
        }

        [Route("api/ApplicationRoles/GetById")]
        public RoleDto PostGetApplicationRoleById(GetApplicationRoleByIdInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.GetApplicationRoleById(inputModel);
        }

        [Route("api/ApplicationRoles/GetByName")]
        public RoleDto PostGetApplicationRoleByName(GetApplicationRoleByNameInputModel inputModel)
        {
            inputModel.Validate(this, MethodBase.GetCurrentMethod().Name);
            return _service.GetApplicationRoleByName(inputModel);
        }

    }
}