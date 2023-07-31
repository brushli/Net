using Abp.Domain.Entities.Auditing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lee.EMS.SYS
{
    [Table("Sys_ModuleActionInRole")]
    [Index(nameof(RoleId), nameof(ModuleId), nameof(ActionId), IsUnique = true)]
    public class ModuleActionInRole : AuditedEntity<long>
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("RoleId")]
        
        public long RoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("ModuleId")]
        public long ModuleId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("ActionId")]
        
        public long ActionId { get; set; }
    }
}
