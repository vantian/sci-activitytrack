using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Bkpm.ActivityTracker.Controllers;
using System.Threading.Tasks;
using Bkpm.ActivityTracker.ActivityServices;
using Bkpm.ActivityTracker.ActivityServices.Dto;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bkpm.ActivityTracker.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : ActivityTrackerControllerBase
    {
        private readonly IActivityDetailsAppService _ActivityDetailsAppService;
        private readonly IActivityGroupAppService _activityGroupAppService;

        public HomeController(IActivityDetailsAppService ActivityDetailsAppService
            , IActivityGroupAppService activityGroupAppService)
        {
            _ActivityDetailsAppService = ActivityDetailsAppService;
            _activityGroupAppService = activityGroupAppService;
        }

        public async Task<ActionResult> Index()
        {
            await setupGroupCol();
            var model = await _activityGroupAppService.GetForDashboard(0);
            return View(model);
        }

        public async Task<ActionResult> GetTimeline(int id)
        {
            var model = await _activityGroupAppService.GetForDashboard(id);
            return PartialView("_Timeline", model);
        }

        private async Task setupGroupCol()
        {
            var groupCol = await _activityGroupAppService.GetAllAsync(new PagedActivityGroupResultRequestDto() { MaxResultCount = int.MaxValue });
            ViewData["groupCol"] = groupCol.Items.Select(e => new SelectListItem() { Text = e.Nama, Value = e.Id.ToString() }).ToList();
        }
    }
}
