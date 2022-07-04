using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bkpm.ActivityTracker.ActivityEntity;
using Bkpm.ActivityTracker.ActivityServices.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkpm.ActivityTracker.ActivityServices
{
    public interface IActivityDetailsAppService : IAsyncCrudAppService< ActivityDetailsDto, int, PagedActivityDetailsResultRequestDto, CreateOrEditActivityDetailsDto, ActivityDetailsDto>
    {
        Task<ActivityDetailsDto> CreateOrUpdateAsync(CreateOrEditActivityDetailsDto input);
        Task<ActivityDetailsDto> GetForView(int id);
        Task<PagedResultDto<ActivityFileDto>> GetLampiranByActivityId(PagedActivityFileResultRequestDto input);
        Task<ActivityFileDto> CreateLampiranByActivityId(CreateActivityFileDto input);
    }
}

