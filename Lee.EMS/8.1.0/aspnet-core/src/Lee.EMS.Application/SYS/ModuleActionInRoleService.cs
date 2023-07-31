
using System;
using Abp.Application.Services;
using Lee.EMS.SYS.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Authorization;
namespace Lee.EMS.SYS
{
    /// <summary>
    /// 模块动作与角色的关联服务
    /// </summary>
    [AbpAuthorize]
    public class ModuleActionInRoleAppService : AsyncCrudAppService<ModuleActionInRole, ModuleActionInRoleDto,long, GetModuleActionInRoleDto,CreateModuleActionInRoleDto,ModuleActionInRoleDto>, IModuleActionInRoleAppService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>     
        public ModuleActionInRoleAppService(IRepository<ModuleActionInRole, long> repository) : base(repository)
        {
            
        }
    }
}