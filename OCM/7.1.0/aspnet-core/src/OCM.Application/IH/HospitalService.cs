using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using OCM.IH.Dto;
using System.Linq;

namespace OCM.IH
{
    /// <summary>
    /// 医院服务
    /// </summary>
    [AbpAuthorize]
    public class HospitalAppService : AsyncCrudAppService<Hospital, HospitalDto, int, GetHospitalDto, CreateHospitalDto, HospitalDto>, IHospitalAppService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>     
        public HospitalAppService(IRepository<Hospital, int> repository) : base(repository)
        {

        }
        protected override IQueryable<Hospital> CreateFilteredQuery(GetHospitalDto input)
        {
            return base.CreateFilteredQuery(input);
        }
    }
}