
using System;
using Abp.Application.Services;
using Lee.EMS.BAS.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Authorization;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Abp.Extensions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Lee.EMS.BAS
{
    /// <summary>
    /// 公共字典服务
    /// </summary>
    [AbpAuthorize]
    public class DictionaryAppService : AsyncCrudAppService<Dictionary, DictionaryDto,long, GetDictionaryDto,CreateDictionaryDto,DictionaryDto>, IDictionaryAppService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>     
        public DictionaryAppService(IRepository<Dictionary, long> repository) : base(repository)
        {
            
        }
        protected override IQueryable<Dictionary> CreateFilteredQuery(GetDictionaryDto input)
        {
            return Repository.GetAll()
               .WhereIf(!input.KeyWord.IsNullOrWhiteSpace(), x => x.Name.Contains(input.KeyWord) || x.Code.Contains(input.KeyWord));
        }
    }
}