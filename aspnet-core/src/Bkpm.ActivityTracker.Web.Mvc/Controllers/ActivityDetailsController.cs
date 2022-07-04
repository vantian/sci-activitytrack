using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Bkpm.ActivityTracker.Authorization;
using Bkpm.ActivityTracker.Controllers;
using Bkpm.ActivityTracker.ActivityServices;
using Bkpm.ActivityTracker.ActivityServices.Dto;
using Bkpm.ActivityTracker.Web.Models.Roles;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;

namespace Bkpm.ActivityTracker.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Roles)]
    public class ActivityDetailsController : ActivityTrackerControllerBase
    {
        private readonly IActivityDetailsAppService _ActivityDetailsAppService;
        private readonly IActivityGroupAppService _activityGroupAppService;
        private IHostingEnvironment Environment;

        public ActivityDetailsController(IActivityDetailsAppService ActivityDetailsAppService
            , IActivityGroupAppService activityGroupAppService
            , IHostingEnvironment environment)
        {
            _ActivityDetailsAppService = ActivityDetailsAppService;
            _activityGroupAppService = activityGroupAppService;
            Environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _ActivityDetailsAppService.GetAllAsync(new PagedActivityDetailsResultRequestDto { MaxResultCount = 20, SkipCount = 0});
            await setupGroupCol();

            return View(model);
        }

        public async Task<ActionResult> View(int id)
        {
            var model = await _ActivityDetailsAppService.GetForView(id);

            if (model == null)
                return RedirectToAction("Index");
            
            return View(model);
        }

        public async Task<ActionResult> CreateOrEditModal(int id)
        {
            var model = new ActivityDetailsDto();
            if (id > 0)
            {
                model = await _ActivityDetailsAppService.GetAsync(new EntityDto<int>(id));
            }
            await setupGroupCol();
            return PartialView("_CreateOrEditModal", model);
        }

        private async Task setupGroupCol()
        {
            var groupCol = await _activityGroupAppService.GetAllAsync(new PagedActivityGroupResultRequestDto() { MaxResultCount = int.MaxValue });
            ViewData["groupCol"] = groupCol.Items.Select(e => new SelectListItem() { Text = e.Nama, Value = e.Id.ToString() }).ToList();
        }


        [HttpPost]
        public async Task<JsonResult> UploadLampiran(ActivityFileDto input)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {
                        if (file != null && file.Length > 0)
                        {
                            var fileStream = file.OpenReadStream();

                            var fileName = Path.GetFileNameWithoutExtension(file.FileName) +
                              "_" + Guid.NewGuid().ToString().Substring(0, 4) + Path.GetExtension(file.FileName);

                            string wwwPath = this.Environment.WebRootPath;
                            string contentPath = this.Environment.ContentRootPath;

                            string path = Path.Combine(this.Environment.WebRootPath, "Uploads", "ActivityDetails", input.ActivityDetailId.ToString());
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                            {
                                fileStream.CopyTo(stream);
                            }

                            await _ActivityDetailsAppService.CreateLampiranByActivityId(new CreateActivityFileDto() { 
                                ActivityDetailId = input.ActivityDetailId,
                                Nama = input.Nama,
                                Url = $"Uploads/ActivityDetails/{input.ActivityDetailId}/{fileName}"
                            });
                        }
                    }
                }
                else
                {
                    throw new Exception("File lampiran diperlukan");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(new { Ok = true });
        }
    }
}
