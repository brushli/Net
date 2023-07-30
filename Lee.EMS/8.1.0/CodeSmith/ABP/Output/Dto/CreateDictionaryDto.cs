
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:CreateDictionaryDto.cs
// 功能描述:公共字典  创建数据对象
//
//
// 创建标识: Lee -- 2023-07-29 22:36:02
//
//------------------------------------------------------*/

using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
 
namespace Lee.EMS.BAS.Dto
{
      /// <summary>
      /// 公共字典 创建对象
      /// </summary>
      [AutoMap(typeof(Dictionary))]
      public class CreateDictionaryDto: IShouldNormalize
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
        
        public void Normalize()
        {
        }
      }
      
}