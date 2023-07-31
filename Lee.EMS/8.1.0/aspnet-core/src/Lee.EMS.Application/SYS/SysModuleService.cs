
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
    /// 模块关联服务
    /// </summary>
    [AbpAuthorize]
    public class SysModuleAppService : AsyncCrudAppService<SysModule, SysModuleDto,long, GetSysModuleDto,CreateSysModuleDto,SysModuleDto>, ISysModuleAppService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>     
        public SysModuleAppService(IRepository<SysModule, long> repository) : base(repository)
        {
            
        }
    }
}