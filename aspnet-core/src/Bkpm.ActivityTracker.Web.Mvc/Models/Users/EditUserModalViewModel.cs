using System.Collections.Generic;
using System.Linq;
using Bkpm.ActivityTracker.Roles.Dto;
using Bkpm.ActivityTracker.Users.Dto;

namespace Bkpm.ActivityTracker.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
        }
    }
}
