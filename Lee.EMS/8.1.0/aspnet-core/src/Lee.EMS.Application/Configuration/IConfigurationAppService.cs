using System.Threading.Tasks;
using Lee.EMS.Configuration.Dto;

namespace Lee.EMS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
