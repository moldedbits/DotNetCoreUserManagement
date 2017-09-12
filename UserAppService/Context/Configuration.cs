using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using UserAppService.Utility.Extensions;

namespace UserAppService.Context
{
    public class Configuration
    {
        public static void Seed(ApplicationDbContext context)
        {

            var seedStart = DateTime.UtcNow;

            if (context.Users.FirstOrDefault(x => x.Id == -1) == null)
            {
                var dateTime = new SqlParameter("@dateTime", seedStart);
                context.Database.ExecuteSqlCommand(@"
                     SET IDENTITY_INSERT [dbo].[User] ON;
                     INSERT INTO [dbo].[User] ([Id], [CreatedOn], [UpdatedOn], [UpdatedById], [CreatedById], [UserName], [EmailConfirmed], [PhoneNumberConfirmed], [LockoutEnabled], [AccessFailedCount], [TwoFactorEnabled]) Values(-1, @dateTime, @dateTime,-1,-1, 'SuperAdmin', 0, 0, 0, 0, 0);
                     SET IDENTITY_INSERT [dbo].[User] OFF;", dateTime);
            }

            var defaultUser = context.Users.FirstOrDefault(x => x.Id == 1);

            if (defaultUser.IsNull())
            {
                context.Users.AddAsync(SeedDataBuilder.BuildDefaultUser());
                context.SaveChanges();
            }

            if (context.Roles.IsNull() || !context.Roles.Any())
            {
                context.Roles.AddRangeAsync(SeedDataBuilder.BuildApplicationRole().ToArray());
                context.SaveChanges();
            }

            // Add default user roles mapping
            if (context.UserRole.IsNull() || !context.UserRole.Any())
            {
                context.UserRole.AddRangeAsync(SeedDataBuilder.BuildDefaultUserRole().ToArray());
                context.SaveChanges();
            }

            // Add platforms data
            if (context.Platform.IsNull() || !context.Platform.Any())
            {
                context.Platform.AddRangeAsync(SeedDataBuilder.BuildPlatform().ToArray());
                context.SaveChanges();
            }
        }
    }
}
