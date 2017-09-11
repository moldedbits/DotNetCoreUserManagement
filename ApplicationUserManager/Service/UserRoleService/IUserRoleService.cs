using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAppService.AuthenticationManagers;
using UserAppService.Dto;
using UserAppService.InputModel;
using UserAppService.Models;
using UserAppService.OutputModels;

namespace UserAppService.Service.UserRoleService
{
    public interface IUserRoleService
    {
        BoolMethodResult AddUserRole(AddUserRoleInputModel inputModel);

        BoolMethodResult AddUserRoles(AddUserRolesInputModel inputModel);

        BoolMethodResult DeleteUserRole(DeleteUserRoleInputModel inputModel);

        BoolMethodResult DeleteUserRoles(DeleteUserRolesInputModel inputModel);

        BoolMethodResult DeleteAllUserRoles(DeleteAllUserRolesInputModel inputModel);

        IList<UserRole> GetUserRolesByUserId(GetUserRolesByUserIdInputModel inputModel);

        void GetApplicationUserManager(ApplicationUserManager userManager);
    }
}
