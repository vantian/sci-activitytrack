using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Bkpm.ActivityTracker.Web.Views
{
    public abstract class ActivityTrackerRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected ActivityTrackerRazorPage()
        {
            LocalizationSourceName = ActivityTrackerConsts.LocalizationSourceName;
        }
    }
}
