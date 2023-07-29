using System.Threading.Tasks;
using Abp.Application.Services;
using Lee.EMS.Authorization.Accounts.Dto;

namespace Lee.EMS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
