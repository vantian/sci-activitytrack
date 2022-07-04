using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Bkpm.ActivityTracker.Configuration;
using Bkpm.ActivityTracker.Web;

namespace Bkpm.ActivityTracker.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ActivityTrackerDbContextFactory : IDesignTimeDbContextFactory<ActivityTrackerDbContext>
    {
        public ActivityTrackerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ActivityTrackerDbContext>();
            
            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ActivityTrackerDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ActivityTrackerConsts.ConnectionStringName));

            return new ActivityTrackerDbContext(builder.Options);
        }
    }
}
