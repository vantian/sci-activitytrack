using Abp.Application.Services;
using Bkpm.ActivityTracker.MultiTenancy.Dto;

namespace Bkpm.ActivityTracker.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

