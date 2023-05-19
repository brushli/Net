
using System;
using Abp.Application.Services;
using OCM.IH.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Authorization;
using Abp.Linq.Extensions;

namespace OCM.IH
{
    /// <summary>
    /// 患者服务
    /// </summary>
    [AbpAuthorize]
    public class PatientAppService : AsyncCrudAppService<Patient, PatientDto, int, GetPatientDto,CreatePatientDto,PatientDto>, IPatientAppService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>     
        public PatientAppService(IRepository<Patient, int> repository) : base(repository)
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<Patient> CreateFilteredQuery(GetPatientDto input)
        {
            var result= base.CreateFilteredQuery(input);
            result = result.WhereIf(input.UserId != 0, w => w.UserId == input.UserId);
            return result;
        }
    }
}