
using System;
using Abp.Application.Services;
using Lee.EMS.SYS.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lee.EMS.SYS
{
    /// <summary>
    /// 动作管理服务
    /// </summary>
    public interface IActionAppService : IAsyncCrudAppService<ActionDto, long, GetActionDto, CreateActionDto, ActionDto>
    {
       

    }
}