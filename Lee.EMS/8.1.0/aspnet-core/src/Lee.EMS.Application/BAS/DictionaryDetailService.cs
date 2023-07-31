
using System;
using Abp.Application.Services;
using Lee.EMS.BAS.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Authorization;
using Abp.Linq.Extensions;

namespace Lee.EMS.BAS
{
    /// <summary>
    /// 公共字典服务
    /// </summary>
    [AbpAuthorize]
    public class DictionaryDetailAppService : AsyncCrudAppService<DictionaryDetail, DictionaryDetailDto,long, GetDictionaryDetailDto,CreateDictionaryDetailDto,DictionaryDetailDto>, IDictionaryDetailAppService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>     
        public DictionaryDetailAppService(IRepository<DictionaryDetail, long> repository) : base(repository)
        {
            
        }
        protected override IQueryable<DictionaryDetail> CreateFilteredQuery(GetDictionaryDetailDto input)
        {
            return Repository.GetAll()
               .WhereIf(input.DictionaryId.HasValue, x => x.DictionaryId== input.DictionaryId.Value);
        }
    }
}