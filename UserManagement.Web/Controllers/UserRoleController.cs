using Autofac.Integration.WebApi;
using System.Collections.Generic;
using System.Web.Http;
using UserAppService;
using UserAppService.CustomFilter;
using UserAppService.Dto;
using UserAppService.InputModel;
using UserAppService.OutputModels;
using UserAppService.Service;

namespace UserManagement.Web.Controllers
{
    [AutofacControllerConfiguration]
    [ModelExceptionFilter]
    public class UserRoleController : BaseAPIController
    {
        private readonly IAuthService _service;
        public UserRoleController(IAuthService service)
        {
            _service = service;
        }

        [Route("api/UserRole/Add")]
        public BoolMethodResult PostAddUserRole(AddUserRoleInputModel inputModel)
        {
            return _service.AddUserRole(inputModel);
        }

        [Route("api/UserRole/AddRoles")]
        public BoolMethodResult PostAddUserRoles(AddUserRolesInputModel inputModel)
        {
            return _service.AddUserRoles(inputModel);
        }

        [Route("api/UserRole/Delete")]
        public BoolMethodResult PostDeleteUserRole(DeleteUserRoleInputModel inputModel)
        {
            return _service.DeleteUserRole(inputModel);
        }

        [Route("api/UserRole/DeleteRoles")]
        public BoolMethodResult PostDeleteUserRoles(DeleteUserRolesInputModel inputModel)
        {
            return _service.DeleteUserRoles(inputModel);
        }

        [Route("api/UserRole/DeleteAll")]
        public BoolMethodResult PostDeleteAllUserRoles(DeleteAllUserRolesInputModel inputModel)
        {
            return _service.DeleteAllUserRoles(inputModel);
        }

        [Route("api/UserRole/GetByUserId")]
        public IList<RoleDto> PostGetUserRolesByUserId(GetUserRolesByUserIdInputModel inputModel)
        {
            return _service.GetUserRolesByUserId(inputModel);
        }
    }
}