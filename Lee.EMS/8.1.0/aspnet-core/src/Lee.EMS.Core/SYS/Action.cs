using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using System.Diagnostics.CodeAnalysis;

namespace Lee.EMS.SYS
{
    [Table("Sys_Action")]
    public  class Action : AuditedEntity<long>
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("ModuleId"), NotNull]
        public long ModuleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Name"), MaxLength(100), NotNull]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Icon"), MaxLength(200)]
        public string Icon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Sort")]
        public long Sort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Visible"), MaxLength(1)]
        public string Visible { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("MethodCode"), MaxLength(100)]
        public string MethodCode { get; set; }
    }
}
