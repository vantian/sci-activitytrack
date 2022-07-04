using System.Collections.Generic;
using Bkpm.ActivityTracker.Roles.Dto;

namespace Bkpm.ActivityTracker.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}