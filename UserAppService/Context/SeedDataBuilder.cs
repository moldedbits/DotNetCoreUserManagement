using System.Collections.Generic;
using UserAppService.Enums;
using UserAppService.Models;

namespace UserAppService.Context
{
    public class SeedDataBuilder
    {
        public static List<ApplicationRole> BuildApplicationRole()
        {
            return new List<ApplicationRole>
            {
                 new ApplicationRole
                  {
                    Id = 1,
                    Name = Role.SUPERADMIN.ToString().ToUpper(),
                    Description = Role.SUPERADMIN.ToString(),
                    CreatedById = -1
                 },
                 new ApplicationRole
                  {
                    Id = 2,
                    Name = Role.ADMIN.ToString().ToUpper(),
                    Description = Role.ADMIN.ToString(),
                    CreatedById = -1
                 }
            };
        }
    
        public static User BuildDefaultUser()
        {
            return new User
            {
                Id = 1,
                UserName = "ABC",
                PasswordHash = "AP02ZhtwHR99UNTAbsjkm5eBIBrWcku0xDgx1lP5w428nzDLuzH5XJtJCDaVvTOlcA==",
                Email = "ABC@gmail.com",
                SecurityStamp = "9a2bf52a - ef66 - 47c4 - 9eeb - b944ca3d1dfb",
                //CreatedById = -1 
            };
        }

        public static List<Platform> BuildPlatform()
        {
            return new List<Platform>
            {
                 new Platform
                  {
                    Id = 1,
                    Name = APIKey.APIKEY_ANDROID.ToString(),
                    IsActive = true,
                    CreatedById = -1
                 },
                 new Platform
                  {
                    Id = 2,
                    Name = APIKey.APIKEY_IOS.ToString(),
                    IsActive = true,
                    CreatedById = -1
                 }
            };
        }

        public static List<UserRole> BuildDefaultUserRole()
        {
            return new List<UserRole>
            {
                 new UserRole
                  {
                    UserId = -1,
                    RoleId = 1
                 },
                 new UserRole
                  {
                    UserId = 1,
                    RoleId = 2
                 }
            };
        }
    }
}
