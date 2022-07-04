using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Bkpm.ActivityTracker.Controllers
{
    public abstract class ActivityTrackerControllerBase: AbpController
    {
        protected ActivityTrackerControllerBase()
        {
            LocalizationSourceName = ActivityTrackerConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
