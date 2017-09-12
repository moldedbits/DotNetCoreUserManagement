using AutoMapper;
using UserAppService.Dto;
using UserAppService.Models;

namespace UserAppService.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationRole, RoleDto>();
            });
        }
    }
}
