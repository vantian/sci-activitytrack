using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using Bkpm.ActivityTracker.Controllers;

namespace Bkpm.ActivityTracker.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : ActivityTrackerControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
