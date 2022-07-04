using Abp.AspNetCore.Mvc.ViewComponents;

namespace Bkpm.ActivityTracker.Web.Views
{
    public abstract class ActivityTrackerViewComponent : AbpViewComponent
    {
        protected ActivityTrackerViewComponent()
        {
            LocalizationSourceName = ActivityTrackerConsts.LocalizationSourceName;
        }
    }
}
