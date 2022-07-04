using Abp.MultiTenancy;
using Bkpm.ActivityTracker.Authorization.Users;

namespace Bkpm.ActivityTracker.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
