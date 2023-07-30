
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:DictionaryDetailDto.cs
// 功能描述:公共字典  编辑对象
//
//
// 创建标识: Lee -- 2023-07-29 22:36:10
//
//------------------------------------------------------*/
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
 
namespace Lee.EMS.BAS.Dto
{
      /// <summary>
      /// 公共字典 编辑对象
      /// </summary>
      [AutoMap(typeof(DictionaryDetail))]
      public class DictionaryDetailDto : EntityDto<long>
      {

		/// <summary>
		/// DictionaryId 
		/// </summary>	
        [Required]
        public long DictionaryId { get; set; }
        
		/// <summary>
		/// Name 
		/// </summary>	
        [Required]
        public string Name { get; set; }
        
		/// <summary>
		/// Value 
		/// </summary>	
        [Required]
        public string Value { get; set; }
        
		/// <summary>
		/// Sort 
		/// </summary>	
        [Required]
        public int Sort { get; set; }
        
		/// <summary>
		/// Describe 
		/// </summary>	
        
        public string Describe { get; set; }
        
		/// <summary>
		/// IsDefualt 
		/// </summary>	
        [Required]
        public bool IsDefualt { get; set; }
        
      }
     
}