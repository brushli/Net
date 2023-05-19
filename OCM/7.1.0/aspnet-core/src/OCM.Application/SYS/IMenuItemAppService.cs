using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using OCM.Roles.Dto;
using OCM.SYS.Dto;

namespace OCM.MenuItems
{
    public interface IMenuItemAppService : IAsyncCrudAppService<MenuItemDto, int, PagedMenuItemResultRequestDto, CreateMenuItemDto, MenuItemDto>
    {
        
    }
}
