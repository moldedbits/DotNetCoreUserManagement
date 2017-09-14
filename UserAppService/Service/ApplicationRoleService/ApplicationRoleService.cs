using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using UserAppService.Context;
using UserAppService.Dto;
using UserAppService.InputModel;
using UserAppService.Models;
using UserAppService.OutputModels;
using UserAppService.Utility.Extensions;

namespace UserAppService.Service
{
    public class ApplicationRoleService : IApplicationRoleService
    {
        private RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public ApplicationRoleService(ApplicationDbContext applicationDbContext, RoleManager<ApplicationRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
        }

        public List<RoleDto> GetApplicationRoles()
        {
            return Mapper.Map<List<RoleDto>>(_roleManager.Roles.ToList());
        }
    }
}
