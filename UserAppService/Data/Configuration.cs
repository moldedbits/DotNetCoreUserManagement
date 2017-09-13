using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using UserAppService.Utility.Extensions;

namespace UserAppService.Context
{
    public class Configuration
    {

        public async static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            var defaultUser = context.Users.FirstOrDefault(x => x.Id == 1);

            if (defaultUser.IsNull())
            {
                await context.Users.AddAsync(SeedDataBuilder.BuildDefaultUser());
                context.SaveChanges();
            }

            if (context.Roles.IsNull() || !context.Roles.Any())
            {
                await context.Roles.AddRangeAsync(SeedDataBuilder.BuildApplicationRole().ToArray());
                context.SaveChanges();
            }

        }
    }
}
