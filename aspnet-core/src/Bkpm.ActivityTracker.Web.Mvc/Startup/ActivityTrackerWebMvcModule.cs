using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Bkpm.ActivityTracker.Configuration;

namespace Bkpm.ActivityTracker.Web.Startup
{
    [DependsOn(typeof(ActivityTrackerWebCoreModule))]
    public class ActivityTrackerWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ActivityTrackerWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<ActivityTrackerNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ActivityTrackerWebMvcModule).GetAssembly());
        }
    }
}
