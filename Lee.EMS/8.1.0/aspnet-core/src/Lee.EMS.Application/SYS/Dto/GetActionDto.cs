
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:GetActionDto.cs
// 功能描述:动作管理  创建数据对象
//
//
// 创建标识: Lee -- 2023-07-30 15:32:44
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
      /// 动作管理 查询对象
      /// </summary>      
      public class GetActionDto:PagedResultRequestDto
      {

		/// <summary>
		/// ModuleId 
		/// </summary>	
        public long? ModuleId { get; set; }
        
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
        public string Visible { get; set; }
        
		/// <summary>
		/// MethodCode 
		/// </summary>	
        public string MethodCode { get; set; }
        
      }
     
}