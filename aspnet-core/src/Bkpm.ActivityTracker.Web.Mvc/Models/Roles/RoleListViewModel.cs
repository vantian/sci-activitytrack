using System.Collections.Generic;
using Bkpm.ActivityTracker.Roles.Dto;

namespace Bkpm.ActivityTracker.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
