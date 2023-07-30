
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:GetDictionaryDetailDto.cs
// 功能描述:公共字典  创建数据对象
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
      /// 公共字典 查询对象
      /// </summary>      
      public class GetDictionaryDetailDto:PagedResultRequestDto
      {

		/// <summary>
		/// DictionaryId 
		/// </summary>	
        public long DictionaryId { get; set; }
        
		/// <summary>
		/// Name 
		/// </summary>	
        public string Name { get; set; }
        
		/// <summary>
		/// Value 
		/// </summary>	
        public string Value { get; set; }
        
		/// <summary>
		/// Sort 
		/// </summary>	
        public int Sort { get; set; }
        
		/// <summary>
		/// Describe 
		/// </summary>	
        public string Describe { get; set; }
        
		/// <summary>
		/// IsDefualt 
		/// </summary>	
        public bool IsDefualt { get; set; }
        
      }
     
}