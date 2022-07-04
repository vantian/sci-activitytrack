using Abp.AutoMapper;
using Bkpm.ActivityTracker.Authentication.External;

namespace Bkpm.ActivityTracker.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
