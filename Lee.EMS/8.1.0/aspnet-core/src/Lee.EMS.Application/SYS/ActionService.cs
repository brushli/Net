
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
    /// 动作管理服务
    /// </summary>
    [AbpAuthorize]
    public class ActionAppService : AsyncCrudAppService<Action, ActionDto,long, GetActionDto,CreateActionDto,ActionDto>, IActionAppService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>     
        public ActionAppService(IRepository<Action, long> repository) : base(repository)
        {
            
        }
    }
}