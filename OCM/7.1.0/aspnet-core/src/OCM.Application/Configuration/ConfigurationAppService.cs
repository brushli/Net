using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using OCM.Configuration.Dto;

namespace OCM.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : OCMAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
