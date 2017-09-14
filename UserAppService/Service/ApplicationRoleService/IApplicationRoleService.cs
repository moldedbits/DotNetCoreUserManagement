using System.Collections.Generic;
using UserAppService.Dto;

namespace UserAppService.Service
{
    public interface IApplicationRoleService
    {
        List<RoleDto> GetApplicationRoles();
    }
}
