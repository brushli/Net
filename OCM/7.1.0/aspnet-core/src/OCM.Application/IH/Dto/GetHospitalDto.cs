
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:GetHospitalDto.cs
// 功能描述:医院  创建数据对象
//
//
// 创建标识: Chad Lee -- 2022-08-27 11:12:54
//
//------------------------------------------------------*/
using Abp.Application.Services.Dto;

namespace OCM.IH.Dto
{
    /// <summary>
    /// 医院 查询对象
    /// </summary>      
    public class GetHospitalDto : PagedResultRequestDto
    {
        /// <summary>
        /// Name 
        /// </summary>	
        public string Name { get; set; }

        /// <summary>
        /// Description 
        /// </summary>	
        public string Description { get; set; }

        /// <summary>
        /// AppId 
        /// </summary>	
        public string AppId { get; set; }

        /// <summary>
        /// AppSecurityKey 
        /// </summary>	
        public string AppSecurityKey { get; set; }

    }

}