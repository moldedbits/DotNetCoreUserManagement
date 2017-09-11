using System.Data.Entity;

namespace UserAppService.Context
{
    public class ApplicationDbInitializer : MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>
    {
        public override void InitializeDatabase(ApplicationDbContext context)
        {
            base.InitializeDatabase(context);
        }

    }
}
