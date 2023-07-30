using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace Lee.EMS.BAS
{
    [Table("Bas_Dictionary")]
    public class Dictionary:AuditedEntity<long>
    {
        [Column("ParentId")]
        public long Parentid { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("Sort")]
        public int Sort { get; set; }

        [Column("Code")]
        [StringLength(36)]
        public string Code { get; set; }

        [Column("Mem")]
        [StringLength(200)]
        public string Mem { get; set; }
    }
}
