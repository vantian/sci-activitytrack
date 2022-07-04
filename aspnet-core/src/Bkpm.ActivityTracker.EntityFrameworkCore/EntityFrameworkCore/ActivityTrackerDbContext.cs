using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Bkpm.ActivityTracker.Authorization.Roles;
using Bkpm.ActivityTracker.Authorization.Users;
using Bkpm.ActivityTracker.MultiTenancy;
using Bkpm.ActivityTracker.ActivityEntity;

namespace Bkpm.ActivityTracker.EntityFrameworkCore
{
    public class ActivityTrackerDbContext : AbpZeroDbContext<Tenant, Role, User, ActivityTrackerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public ActivityTrackerDbContext(DbContextOptions<ActivityTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<ActivityGroup> ActivityGroup { get; set; }
        public DbSet<ActivityFiles> ActivityFiles { get; set; }
        public DbSet<ActivityDetails> ActivityDetails { get; set; }
        public DbSet<ActivityLog> ActivityLog { get; set; }
    }
}
