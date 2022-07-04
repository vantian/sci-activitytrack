using System.Threading.Tasks;
using Bkpm.ActivityTracker.Configuration.Dto;

namespace Bkpm.ActivityTracker.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
