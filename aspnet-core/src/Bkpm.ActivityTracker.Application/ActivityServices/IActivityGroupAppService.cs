using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Bkpm.ActivityTracker.ActivityEntity;
using Bkpm.ActivityTracker.ActivityServices.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace Bkpm.ActivityTracker.ActivityServices
{
    public interface IActivityGroupAppService : IAsyncCrudAppService< ActivityGroupDto, int, PagedActivityGroupResultRequestDto, CreateOrEditActivityGroupDto, ActivityGroupDto>
    {
        Task<ActivityGroupDto> CreateOrUpdateAsync(CreateOrEditActivityGroupDto input);
        Task<ActivityGroupDashboardDto> GetForDashboard(int id);
    }
}

