using Abp.Application.Services.Dto;

namespace Bkpm.ActivityTracker.ActivityServices.Dto
{
    public class PagedActivityGroupResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

