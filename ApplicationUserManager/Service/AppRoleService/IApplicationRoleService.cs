using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAppService.Dto;
using UserAppService.InputModel;
using UserAppService.OutputModels;

namespace UserAppService.Service
{
    public interface IApplicationRoleService
    {
        BoolMethodResult AddApplicationRole(AddApplicationRoleInputModel inputModel);

        BoolMethodResult UpdateApplicationRole(UpdateApplicationRoleInputModel inputModel);

        BoolMethodResult DeleteApplicationRole(DeleteApplicationRoleInputModel inputModel);

        List<RoleDto> GetApplicationRoles();

        RoleDto GetApplicationRoleById(GetApplicationRoleByIdInputModel inputModel);

        RoleDto GetApplicationRoleByName(GetApplicationRoleByNameInputModel inputModel);
    }
}
