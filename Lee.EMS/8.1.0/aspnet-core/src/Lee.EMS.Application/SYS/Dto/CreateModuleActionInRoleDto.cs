
/*-------------------------------------------------------
// Copyright (C) 2019 Lic
//
// 文件名:CreateModuleActionInRoleDto.cs
// 功能描述:模块动作与角色的关联  创建数据对象
//
//
// 创建标识: Lee -- 2023-07-30 15:33:18
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
      /// 模块动作与角色的关联 创建对象
      /// </summary>
      [AutoMap(typeof(ModuleActionInRole))]
      public class CreateModuleActionInRoleDto: IShouldNormalize
      {

		/// <summary>
		/// RoleId 
		/// </summary>	
        [Required]
        public long RoleId { get; set; }
        
		/// <summary>
		/// ModuleId 
		/// </summary>	
        [Required]
        public long ModuleId { get; set; }
        
		/// <summary>
		/// ActionId 
		/// </summary>	
        [Required]
        public long ActionId { get; set; }
        
        public void Normalize()
        {
        }
      }
      
}