using Abp.Domain.Entities.Auditing;
using OCM.Core.DBAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace OCM.IH
{
    /// <summary>
    /// 就诊患者
    /// </summary>
    public class Patient : FullAuditedEntity<int>
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        [StringLength(50)]
        [Required]
        public string PatientName { set; get; }
        /// <summary>
        /// 性别 1男 2女 3未说明性别 9 其他
        /// </summary>
        [StringLength(2)]
        [Required]
        public string Sex { set; get; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { set; get; }
        /// <summary>
        /// 证件类型：1 身份证 
        /// </summary>
        [StringLength(2)]
        [Required]
        public string IdClass { set; get; }
        /// <summary>
        /// 证件号码
        /// </summary>
        [StringLength(30)]
        [Required]
        public string IdCard { set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(20)]
        [Required]
        public string Phone { set; get; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Address { set; get; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { set; get; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { set; get; }
        /// <summary>
        /// 县
        /// </summary>
        public string County { set; get; }
        /// <summary>
        /// 乡镇
        /// </summary>
        public string Town { set; get; }
        /// <summary>
        /// 街道/村--末尾的详细地址
        /// </summary>
        public string Street { set; get; }
        /// <summary>
        /// 检索码
        /// </summary>
        public string RetrievalCode { set; get; }
        /// <summary>
        /// 用户ID，和user对应
        /// </summary>
        [Required]
        public int UserId { set; get; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DecimalPrecision(4, 1)]
        public decimal? Age { set; get; }
        /// <summary>
        /// HIS系统的ID
        /// </summary>
        public string HisId { set; get; }
        /// <summary>
        /// 归类：1 成人  2、儿童 3、孕妇 4、老人
        /// </summary>
        public string Classify { set; get; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SerialNumber { set; get; }

    }
}
