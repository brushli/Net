
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:SysModuleDto.cs
// 功能描述:模块关联  编辑对象
//
//
// 创建标识: Lee -- 2023-07-30 15:48:53
//
//------------------------------------------------------*/
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
 
namespace Lee.EMS.SYS.Dto
{
      /// <summary>
      /// 模块关联 编辑对象
      /// </summary>
      [AutoMap(typeof(SysModule))]
      public class SysModuleDto : EntityDto<long>
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
        
        public bool Visible { get; set; }
        
		/// <summary>
		/// PageRoute 
		/// </summary>	
        
        public string PageRoute { get; set; }
        
		/// <summary>
		/// Describe 
		/// </summary>	
        
        public string Describe { get; set; }
        
      }
     
}