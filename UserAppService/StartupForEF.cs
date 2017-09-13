using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserAppService.Context;
using Microsoft.EntityFrameworkCore.Design;

namespace UserAppService
{
    /// <summary>
    /// The whole point of this class is to allow for EF Core code to exist in a seperate project from the web application
    /// and be able to do migrations and so forth
    /// https://github.com/aspnet/EntityFramework/issues/7889   -- using this fix
    /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext
    /// </summary>
    /// 
    class StartupForEF : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var confBuilder = new ConfigurationBuilder()
           .AddEnvironmentVariables()
           .AddUserSecrets<StartupForEF>();

            IConfiguration Configuration = confBuilder.Build();

            // This is for a mutli-tenant Environment, so the ConnectionString Env Var name can be set
            // In AppSettings.json
            var ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=UserManagement;Integrated Security=True";

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseSqlServer(ConnectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}
