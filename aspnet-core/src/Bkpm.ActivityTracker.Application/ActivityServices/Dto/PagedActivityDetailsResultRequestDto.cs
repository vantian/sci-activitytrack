using Abp.Application.Services.Dto;

namespace Bkpm.ActivityTracker.ActivityServices.Dto
{
    public class PagedActivityDetailsResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public int? ActivityGroupId { get; set; }
    }
}

