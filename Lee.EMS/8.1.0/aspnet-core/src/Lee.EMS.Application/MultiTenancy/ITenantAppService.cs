using Abp.Application.Services;
using Lee.EMS.MultiTenancy.Dto;

namespace Lee.EMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

