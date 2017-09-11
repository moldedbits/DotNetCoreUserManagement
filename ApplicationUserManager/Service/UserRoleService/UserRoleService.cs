using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using UserAppService.AuthenticationManagers;
using UserAppService.Dto;
using UserAppService.Exceptions;
using UserAppService.InputModel;
using UserAppService.Models;
using UserAppService.OutputModels;
using UserAppService.Utility.Extensions;

namespace UserAppService.Service.UserRoleService
{
    public class UserRoleService : IUserRoleService
    {
        private ApplicationUserManager _userManager;

        public void GetApplicationUserManager(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public BoolMethodResult AddUserRole(AddUserRoleInputModel inputModel)
        {
            var outputModel = new BoolMethodResult();

            try
            {
                var res = _userManager.AddToRole(inputModel.UserId, inputModel.RoleName);

                outputModel.WasSuccessful = res.Succeeded;
                outputModel.Status = ResourceFiles.LocalizedText.UserRoleGranted;
            }
            catch( System.InvalidOperationException ex )
            {
                outputModel.Status = ResourceFiles.LocalizedText.UserOrRoleNotExist;
            }
            catch( System.Data.Entity.Infrastructure.DbUpdateException ex )
            {
                outputModel.Status = ResourceFiles.LocalizedText.UserRoleExists;
            }
            catch( Exception ex )
            {
                outputModel.Status = $"Some error occurred: {ex.Message}";
            }

            return outputModel;
        }

        public BoolMethodResult AddUserRoles(AddUserRolesInputModel inputModel)
        {
            var outputModel = new BoolMethodResult();

            try
            {
                var roles = RegexExtensions.RemoveDirtySpaces(inputModel.RoleNames).Split(new char [] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();

                var res = _userManager.AddToRoles(inputModel.UserId, roles);

                if( res.Errors.Any() )
                {
                    outputModel.Status = ResourceFiles.LocalizedText.UserRoleExists;
                }
                else
                {
                    outputModel.WasSuccessful = res.Succeeded;
                    outputModel.Status = ResourceFiles.LocalizedText.UserRoleGranted;
                }
            }
            catch( System.InvalidOperationException ex )
            {
                outputModel.Status = ResourceFiles.LocalizedText.UserOrRoleNotExist;
            }
            catch( System.Data.Entity.Infrastructure.DbUpdateException ex )
            {
                outputModel.Status = ResourceFiles.LocalizedText.UserRoleExists;
            }
            catch( Exception ex )
            {
                outputModel.Status = $"Some error occurred: {ex.Message}";
            }

            return outputModel;
        }

        public BoolMethodResult DeleteUserRole(DeleteUserRoleInputModel inputModel)
        {
            var outputModel = new BoolMethodResult();

            try
            {
                var res = _userManager.RemoveFromRole(inputModel.UserId, inputModel.RoleName);

                if( res.Errors.Any() )
                {
                    outputModel.Status = res.Errors.First().ToString();
                }
                else
                {
                    outputModel.WasSuccessful = res.Succeeded;
                    outputModel.Status = ResourceFiles.LocalizedText.UserRoleDeleted;
                }
            }
            catch( System.InvalidOperationException ex )
            {
                outputModel.Status = ResourceFiles.LocalizedText.UserOrRoleNotExist;
            }
            catch( Exception ex )
            {
                outputModel.Status = $"Some error occurred: {ex.Message}";
            }

            return outputModel;
        }

        public BoolMethodResult DeleteUserRoles(DeleteUserRolesInputModel inputModel)
        {
            var outputModel = new BoolMethodResult();

            try
            {
                var roles = RegexExtensions.RemoveDirtySpaces(inputModel.RoleNames).Split(new char [] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToUpper().Trim()).ToArray();

                var res = _userManager.RemoveFromRoles(inputModel.UserId, roles);

                if( res.Errors.Any() )
                {
                    outputModel.Status = res.Errors.First().ToString();
                }
                else
                {
                    outputModel.WasSuccessful = res.Succeeded;
                    outputModel.Status = ResourceFiles.LocalizedText.UserRoleDeleted;
                }
            }
            catch( System.InvalidOperationException ex )
            {
                outputModel.Status = ResourceFiles.LocalizedText.UserOrRoleNotExist;
            }
            catch( Exception ex )
            {
                outputModel.Status = $"Some error occurred: {ex.Message}";
            }

            return outputModel;
        }

        public BoolMethodResult DeleteAllUserRoles(DeleteAllUserRolesInputModel inputModel)
        {
            var outputModel = new BoolMethodResult();

            try
            {
                var roles = _userManager.GetRoles(inputModel.UserId).ToArray();

                var res = _userManager.RemoveFromRoles(inputModel.UserId, roles);

                if( res.Errors.Any() )
                {
                    outputModel.Status = res.Errors.First().ToString();
                }
                else
                {
                    outputModel.WasSuccessful = res.Succeeded;
                    outputModel.Status = ResourceFiles.LocalizedText.UserRoleDeleted;
                }
            }
            catch( System.InvalidOperationException ex )
            {
                outputModel.Status = ResourceFiles.LocalizedText.UserNotExist;
            }
            catch( Exception ex )
            {
                outputModel.Status = $"Some error occurred: {ex.Message}";
            }

            return outputModel;
        }

        public IList<UserRole> GetUserRolesByUserId(GetUserRolesByUserIdInputModel inputModel)
        {
            var user = _userManager.FindById(inputModel.UserId);

            if(user.IsNull())
            {
                throw new UserFriendlyException(System.Net.HttpStatusCode.InternalServerError.ToString(), ResourceFiles.LocalizedText.UserNotExist);
            }

            return user.Roles.ToList();
        }
    }
}
