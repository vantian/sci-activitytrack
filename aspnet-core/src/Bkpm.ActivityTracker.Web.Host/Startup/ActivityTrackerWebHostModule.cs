using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Bkpm.ActivityTracker.Configuration;

namespace Bkpm.ActivityTracker.Web.Host.Startup
{
    [DependsOn(
       typeof(ActivityTrackerWebCoreModule))]
    public class ActivityTrackerWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ActivityTrackerWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ActivityTrackerWebHostModule).GetAssembly());
        }
    }
}
