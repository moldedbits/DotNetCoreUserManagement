//using AutoMapper;
//using System.Collections.Generic;
//using System.Linq;
//using UserAppService.Context;
//using UserAppService.Dto;
//using UserAppService.InputModel;
//using UserAppService.Models;
//using UserAppService.OutputModels;
//using UserAppService.Utility.Extensions;

//namespace UserAppService.Service
//{
//    public class ApplicationRoleService : IApplicationRoleService
//    {
//        private ApplicationRoleManager _roleManager;
//        private readonly ApplicationDbContext _applicationDbContext;

//        public ApplicationRoleService(ApplicationDbContext applicationDbContext, ApplicationRoleManager roleManager)
//        {
//            _applicationDbContext = applicationDbContext;
//            _roleManager = roleManager;
//        }

//        public BoolMethodResult AddApplicationRole(AddApplicationRoleInputModel inputModel)
//        {
//            var roleName = RegexExtensions.RemoveDirtySpaces(inputModel.Name.Trim()).ToUpper();

//            var role = new ApplicationRole(roleName, inputModel.Description);

//            var outputModel = new BoolMethodResult();

//            if( _roleManager.RoleExists(roleName) )
//            {
//                outputModel.Status = string.Format(ResourceFiles.LocalizedText.RoleExists, roleName);
//            }
//            else
//            {
//                var res = _roleManager.Create(new ApplicationRole(roleName, inputModel.Description));

//                outputModel.WasSuccessful = res.Succeeded;
//                outputModel.Status = string.Format(ResourceFiles.LocalizedText.RoleSaved, roleName);
//            }

//            return outputModel;
//        }

//        public BoolMethodResult UpdateApplicationRole(UpdateApplicationRoleInputModel inputModel)
//        {
//            var roleName = RegexExtensions.RemoveDirtySpaces(inputModel.Name.Trim());

//            var role = new ApplicationRole(roleName, inputModel.Description);

//            var outputModel = new BoolMethodResult();

//            var oldRole = _roleManager.FindById(inputModel.Id);

//            if( oldRole != null )
//            {
//                oldRole.Name = roleName;
//                oldRole.Description = inputModel.Description;
//                var updatedResult = _roleManager.Update(oldRole);

//                outputModel.WasSuccessful = updatedResult.Succeeded;
//                outputModel.Status = string.Format(ResourceFiles.LocalizedText.RoleSaved, role.Name);
//            }
//            else
//            {
//                outputModel.Status = string.Format(ResourceFiles.LocalizedText.RoleForRoleIdNotExist, inputModel.Id);
//            }

//            return outputModel;
//        }

//        public BoolMethodResult DeleteApplicationRole(DeleteApplicationRoleInputModel inputModel)
//        {
//            var outputModel = new BoolMethodResult();

//            var roleToBeDeleted = _roleManager.FindById(inputModel.Id);

//            if( roleToBeDeleted != null )
//            {
//                var result = _roleManager.Delete(roleToBeDeleted);

//                outputModel.WasSuccessful = result.Succeeded;
//                outputModel.Status = string.Format(ResourceFiles.LocalizedText.RoleDeleted, roleToBeDeleted.Name);
//            }
//            else
//            {
//                outputModel.Status = string.Format(ResourceFiles.LocalizedText.RoleForRoleIdNotExist, inputModel.Id);
//            }

//            return outputModel;
//        }

//        public List<RoleDto> GetApplicationRoles()
//        {
//            return Mapper.Map<List<RoleDto>>(_roleManager.Roles.ToList());
//        }

//        public RoleDto GetApplicationRoleById(GetApplicationRoleByIdInputModel inputModel)
//        {
//            var res = _roleManager.FindById(inputModel.Id);

//            return Mapper.Map<RoleDto>(res);
//        }

//        public RoleDto GetApplicationRoleByName(GetApplicationRoleByNameInputModel inputModel)
//        {
//            var res = _roleManager.FindByName(inputModel.Name);

//            return Mapper.Map<RoleDto>(res);
//        }
//    }
//}
