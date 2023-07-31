
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:GetModuleActionInRoleDto.cs
// 功能描述:模块动作与角色的关联  创建数据对象
//
//
// 创建标识: Lee -- 2023-07-30 15:33:18
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
      /// 模块动作与角色的关联 查询对象
      /// </summary>      
      public class GetModuleActionInRoleDto:PagedResultRequestDto
      {

		/// <summary>
		/// RoleId 
		/// </summary>	
        public long? RoleId { get; set; }
        
		/// <summary>
		/// ModuleId 
		/// </summary>	
        public long? ModuleId { get; set; }
        
		/// <summary>
		/// ActionId 
		/// </summary>	
        public long? ActionId { get; set; }
        
      }
     
}