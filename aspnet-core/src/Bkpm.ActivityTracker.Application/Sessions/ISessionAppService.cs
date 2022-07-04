using System.Threading.Tasks;
using Abp.Application.Services;
using Bkpm.ActivityTracker.Sessions.Dto;

namespace Bkpm.ActivityTracker.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
