using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using UserAppService.Utility.Extensions;

namespace UserAppService.Context
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            try
            {
                var seedStart = DateTime.UtcNow;
                context.UserId = -1;

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
                    context.Users.AddOrUpdate(user => user.Id, SeedDataBuilder.BuildDefaultUser());
                    context.SaveChanges();
                }

                if (context.Roles.IsNull() || !context.Roles.Any())
                {
                    context.Roles.AddOrUpdate(role => role.Name, SeedDataBuilder.BuildApplicationRole().ToArray());
                }

                // Add default user roles mapping
                if (context.UserRole.IsNull() || !context.UserRole.Any())
                {
                    context.UserRole.AddOrUpdate(ur => ur.UserId, SeedDataBuilder.BuildDefaultUserRole().ToArray());
                }

                // Add platforms data
                if (context.Platform.IsNull() || !context.Platform.Any())
                {
                    context.Platform.AddOrUpdate(p => p.Name, SeedDataBuilder.BuildPlatform().ToArray());
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach(var validationError in ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors))
                {
                    System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " +
                                                       validationError.ErrorMessage);
                }
            }

            base.Seed(context);
        }

    }
}
