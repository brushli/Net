using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Lee.EMS.Configuration.Dto;

namespace Lee.EMS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : EMSAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
