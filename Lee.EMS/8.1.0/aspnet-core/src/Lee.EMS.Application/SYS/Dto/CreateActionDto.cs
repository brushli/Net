
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:CreateActionDto.cs
// 功能描述:动作管理  创建数据对象
//
//
// 创建标识: Lee -- 2023-07-30 15:32:44
//
//------------------------------------------------------*/

using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
 
namespace Lee.EMS.SYS.Dto
{
      /// <summary>
      /// 动作管理 创建对象
      /// </summary>
      [AutoMap(typeof(Action))]
      public class CreateActionDto: IShouldNormalize
      {

		/// <summary>
		/// ModuleId 
		/// </summary>	
        [Required]
        public long ModuleId { get; set; }
        
		/// <summary>
		/// Name 
		/// </summary>	
        [Required]
        public string Name { get; set; }
        
		/// <summary>
		/// Icon 
		/// </summary>	
        
        public string Icon { get; set; }
        
		/// <summary>
		/// Sort 
		/// </summary>	
        [Required]
        public long Sort { get; set; }
        
		/// <summary>
		/// Visible 
		/// </summary>	
        
        public string Visible { get; set; }
        
		/// <summary>
		/// MethodCode 
		/// </summary>	
        
        public string MethodCode { get; set; }
        
        public void Normalize()
        {
        }
      }
      
}