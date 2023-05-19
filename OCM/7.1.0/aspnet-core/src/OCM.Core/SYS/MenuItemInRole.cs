using Abp.Domain.Entities.Auditing;
using OCM.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCM.SYS
{
    /// <summary>
    /// 角色拥有的菜单
    /// </summary>
    public class MenuItemInRole : FullAuditedEntity<int>
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        public int RoleId { set; get; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Required]
        public int MenuItemId { set; get; }
    }
}
