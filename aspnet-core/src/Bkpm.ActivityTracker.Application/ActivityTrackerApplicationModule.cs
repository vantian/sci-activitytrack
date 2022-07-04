using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Bkpm.ActivityTracker.Authorization;

namespace Bkpm.ActivityTracker
{
    [DependsOn(
        typeof(ActivityTrackerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ActivityTrackerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ActivityTrackerAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ActivityTrackerApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
