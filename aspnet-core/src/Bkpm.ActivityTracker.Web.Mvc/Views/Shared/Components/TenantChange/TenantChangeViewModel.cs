using Abp.AutoMapper;
using Bkpm.ActivityTracker.Sessions.Dto;

namespace Bkpm.ActivityTracker.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
