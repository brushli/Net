using System.Threading.Tasks;
using OCM.Configuration.Dto;

namespace OCM.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
