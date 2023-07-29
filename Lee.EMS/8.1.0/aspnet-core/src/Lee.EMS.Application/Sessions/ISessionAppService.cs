using System.Threading.Tasks;
using Abp.Application.Services;
using Lee.EMS.Sessions.Dto;

namespace Lee.EMS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
