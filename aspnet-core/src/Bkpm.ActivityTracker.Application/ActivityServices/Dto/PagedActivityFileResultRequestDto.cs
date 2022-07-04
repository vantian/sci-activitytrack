using Abp.Application.Services.Dto;

namespace Bkpm.ActivityTracker.ActivityServices.Dto
{
    public class PagedActivityFileResultRequestDto : PagedResultRequestDto
    {
        public int? ActivityDetailId { get; set; }
    }
}

