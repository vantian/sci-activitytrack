using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Bkpm.ActivityTracker.Configuration.Dto;

namespace Bkpm.ActivityTracker.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ActivityTrackerAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
