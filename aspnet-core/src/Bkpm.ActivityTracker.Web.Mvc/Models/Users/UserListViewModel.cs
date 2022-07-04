using System.Collections.Generic;
using Bkpm.ActivityTracker.Roles.Dto;

namespace Bkpm.ActivityTracker.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
