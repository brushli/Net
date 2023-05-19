using System.Threading.Tasks;
using Abp.Application.Services;
using OCM.Sessions.Dto;

namespace OCM.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
