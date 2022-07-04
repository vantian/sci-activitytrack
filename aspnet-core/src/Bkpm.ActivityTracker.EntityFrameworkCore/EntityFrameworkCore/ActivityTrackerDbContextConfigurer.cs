using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Bkpm.ActivityTracker.EntityFrameworkCore
{
    public static class ActivityTrackerDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ActivityTrackerDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ActivityTrackerDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
