
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:GetDictionaryDto.cs
// 功能描述:公共字典  创建数据对象
//
//
// 创建标识: Lee -- 2023-07-29 22:36:02
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
      public class GetDictionaryDto:PagedResultRequestDto
      {

		/// <summary>
		/// ParentId 
		/// </summary>	
        public long ParentId { get; set; }
        
		/// <summary>
		/// Name 
		/// </summary>	
        public string Name { get; set; }
        
		/// <summary>
		/// Sort 
		/// </summary>	
        public int Sort { get; set; }
        
		/// <summary>
		/// Code 
		/// </summary>	
        public string Code { get; set; }
        
		/// <summary>
		/// Mem 
		/// </summary>	
        public string Mem { get; set; }
        
      }
     
}