
using System;
using Abp.Application.Services;
using Lee.EMS.SYS.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lee.EMS.SYS
{
    /// <summary>
    /// 模块关联服务
    /// </summary>
    public interface ISysModuleAppService : IAsyncCrudAppService<SysModuleDto, long, GetSysModuleDto, CreateSysModuleDto, SysModuleDto>
    {
       

    }
}