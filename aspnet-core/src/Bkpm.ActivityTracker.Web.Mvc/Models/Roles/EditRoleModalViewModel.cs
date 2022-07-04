using Abp.AutoMapper;
using Bkpm.ActivityTracker.Roles.Dto;
using Bkpm.ActivityTracker.Web.Models.Common;

namespace Bkpm.ActivityTracker.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
