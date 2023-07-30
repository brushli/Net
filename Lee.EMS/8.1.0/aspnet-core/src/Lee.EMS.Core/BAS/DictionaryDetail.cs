using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lee.EMS.BAS
{
    [Table("Bas_DictionaryDetail")]
    public class DictionaryDetail : AuditedEntity<long>
    {
        [Column("DictionaryId")]
        [Required]
        public long DictionaryId { get; set; }

        [Column("Name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("Value")]
        [Required]
        [StringLength(50)]
        public string Value { get; set; }

        [Column("Sort")]
        public int Sort { get; set; }

        [Column("Describe")]
        [StringLength(250)]
        public string Describe { get; set; }
        [Column("IsDefualt")]
        public bool IsDefualt { get; set; }
    }
}
