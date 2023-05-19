
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:CreateHospitalDto.cs
// 功能描述:医院  创建数据对象
//
//
// 创建标识: Chad Lee -- 2022-08-27 11:12:53
//
//------------------------------------------------------*/

using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace OCM.IH.Dto
{
    /// <summary>
    /// 医院 创建对象
    /// </summary>
    [AutoMap(typeof(Hospital))]
    public class CreateHospitalDto : IShouldNormalize
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

        public void Normalize()
        {
        }
    }

}