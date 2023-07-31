
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:GetSysModuleDto.cs
// 功能描述:模块关联  创建数据对象
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
      /// 模块关联 查询对象
      /// </summary>      
      public class GetSysModuleDto:PagedResultRequestDto
      {

		/// <summary>
		/// ParentId 
		/// </summary>	
        public long? ParentId { get; set; }
        
		/// <summary>
		/// Name 
		/// </summary>	
        public string Name { get; set; }
        
		/// <summary>
		/// Icon 
		/// </summary>	
        public string Icon { get; set; }
        
		/// <summary>
		/// Sort 
		/// </summary>	
        public long? Sort { get; set; }
        
		/// <summary>
		/// Visible 
		/// </summary>	
        public bool? Visible { get; set; }
        
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