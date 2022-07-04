using Abp.Application.Services.Dto;

namespace Bkpm.ActivityTracker.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

