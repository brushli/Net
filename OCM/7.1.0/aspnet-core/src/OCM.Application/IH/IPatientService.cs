
using System;
using Abp.Application.Services;
using OCM.IH.Dto;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OCM.IH
{
    /// <summary>
    /// 患者服务
    /// </summary>
    public interface IPatientAppService : IAsyncCrudAppService<PatientDto, int, GetPatientDto, CreatePatientDto, PatientDto>
    {
       

    }
}