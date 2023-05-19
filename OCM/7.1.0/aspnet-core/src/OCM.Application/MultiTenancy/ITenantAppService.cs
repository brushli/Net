using Abp.Application.Services;
using OCM.MultiTenancy.Dto;

namespace OCM.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

