
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:DictionaryDto.cs
// 功能描述:公共字典  编辑对象
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
      /// 公共字典 编辑对象
      /// </summary>
      [AutoMap(typeof(Dictionary))]
      public class DictionaryDto : EntityDto<long>
      {

		/// <summary>
		/// ParentId 
		/// </summary>	
        [Required]
        public long ParentId { get; set; }
        
		/// <summary>
		/// Name 
		/// </summary>	
        [Required]
        public string Name { get; set; }
        
		/// <summary>
		/// Sort 
		/// </summary>	
        [Required]
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