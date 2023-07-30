
using System;
using Abp.Application.Services;
using Lee.EMS.BAS.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Lee.EMS.BAS
{
    /// <summary>
    /// 公共字典服务
    /// </summary>
    public interface IDictionaryDetailAppService : IAsyncCrudAppService<DictionaryDetailDto, long, GetDictionaryDetailDto, CreateDictionaryDetailDto, DictionaryDetailDto>
    {
       

    }
}