
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:PatientDto.cs
// 功能描述:患者  编辑对象
//
//
// 创建标识: Chad Lee -- 2022-08-27 11:16:30
//
//------------------------------------------------------*/
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace OCM.IH.Dto
{
    /// <summary>
    /// 患者 编辑对象
    /// </summary>
    [AutoMap(typeof(Patient))]
    public class PatientDto : EntityDto<int>
    {

        /// <summary>
        /// PatientName 
        /// </summary>	
        [Required]
        public string PatientName { get; set; }

        /// <summary>
        /// Sex 
        /// </summary>	
        [Required]
        public string Sex { get; set; }

        /// <summary>
        /// BirthDate 
        /// </summary>	

        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// IdClass 
        /// </summary>	
        [Required]
        public string IdClass { get; set; }

        /// <summary>
        /// IdCard 
        /// </summary>	
        [Required]
        public string IdCard { get; set; }

        /// <summary>
        /// Phone 
        /// </summary>	
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Address 
        /// </summary>	
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Province 
        /// </summary>	

        public string Province { get; set; }

        /// <summary>
        /// City 
        /// </summary>	

        public string City { get; set; }

        /// <summary>
        /// County 
        /// </summary>	

        public string County { get; set; }

        /// <summary>
        /// Town 
        /// </summary>	

        public string Town { get; set; }

        /// <summary>
        /// Street 
        /// </summary>	

        public string Street { get; set; }

        /// <summary>
        /// RetrievalCode 
        /// </summary>	

        public string RetrievalCode { get; set; }

        /// <summary>
        /// UserId 
        /// </summary>	
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Age 
        /// </summary>	

        public decimal Age { get; set; }

        /// <summary>
        /// HisId 
        /// </summary>	

        public string HisId { get; set; }

        /// <summary>
        /// Classify 
        /// </summary>	

        public string Classify { get; set; }

        /// <summary>
        /// SerialNumber 
        /// </summary>	
        [Required]
        public int SerialNumber { get; set; }

    }

}