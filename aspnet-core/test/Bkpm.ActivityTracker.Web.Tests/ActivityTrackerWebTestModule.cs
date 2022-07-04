using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Bkpm.ActivityTracker.EntityFrameworkCore;
using Bkpm.ActivityTracker.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Bkpm.ActivityTracker.Web.Tests
{
    [DependsOn(
        typeof(ActivityTrackerWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class ActivityTrackerWebTestModule : AbpModule
    {
        public ActivityTrackerWebTestModule(ActivityTrackerEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ActivityTrackerWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ActivityTrackerWebMvcModule).Assembly);
        }
    }
}