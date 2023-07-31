using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Lee.EMS.SYS
{
    [Table("Sys_Module")]
    public class SysModule : AuditedEntity<long>
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("ParentId")]
        public long ParentId { get; set; }
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
        [Column("Visible")]
        public bool Visible { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("PageRoute"), MaxLength(100)]
        public string PageRoute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("Describe"), MaxLength(500)]
        public string Describe { get; set; }
    }
}
