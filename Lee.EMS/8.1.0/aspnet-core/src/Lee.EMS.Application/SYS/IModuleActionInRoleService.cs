
using System;
using Abp.Application.Services;
using Lee.EMS.SYS.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lee.EMS.SYS
{
    /// <summary>
    /// 模块动作与角色的关联服务
    /// </summary>
    public interface IModuleActionInRoleAppService : IAsyncCrudAppService<ModuleActionInRoleDto, long, GetModuleActionInRoleDto, CreateModuleActionInRoleDto, ModuleActionInRoleDto>
    {
       

    }
}