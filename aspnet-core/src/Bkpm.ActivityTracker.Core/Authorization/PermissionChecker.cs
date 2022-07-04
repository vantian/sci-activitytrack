using Abp.Authorization;
using Bkpm.ActivityTracker.Authorization.Roles;
using Bkpm.ActivityTracker.Authorization.Users;

namespace Bkpm.ActivityTracker.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
