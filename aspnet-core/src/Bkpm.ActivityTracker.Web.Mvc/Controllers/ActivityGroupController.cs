using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Bkpm.ActivityTracker.Authorization;
using Bkpm.ActivityTracker.Controllers;
using Bkpm.ActivityTracker.ActivityServices;
using Bkpm.ActivityTracker.ActivityServices.Dto;
using Bkpm.ActivityTracker.Web.Models.Roles;

namespace Bkpm.ActivityTracker.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class ActivityGroupController : ActivityTrackerControllerBase
    {
        private readonly IActivityGroupAppService _activityGroupAppService;

        public ActivityGroupController(IActivityGroupAppService activityGroupAppService)
        {
            _activityGroupAppService = activityGroupAppService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _activityGroupAppService.GetAllAsync(new PagedActivityGroupResultRequestDto { MaxResultCount = 20, SkipCount = 0});

            return View(model);
        }

        public async Task<ActionResult> CreateOrEditModal(int id)
        {
            var model = new ActivityGroupDto();
            if (id > 0)
            {
                model = await _activityGroupAppService.GetAsync(new EntityDto<int>(id));
            }

            return PartialView("_CreateOrEditModal", model);
        }
    }
}
