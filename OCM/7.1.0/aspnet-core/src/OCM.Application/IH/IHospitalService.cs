using Abp.Application.Services;
using OCM.IH.Dto;

namespace OCM.IH
{
    /// <summary>
    /// 医院服务
    /// </summary>
    public interface IHospitalAppService : IAsyncCrudAppService<HospitalDto, int, GetHospitalDto, CreateHospitalDto, HospitalDto>
    {


    }
}