using System.Threading.Tasks;
using Abp.Application.Services;
using Bkpm.ActivityTracker.Authorization.Accounts.Dto;

namespace Bkpm.ActivityTracker.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
