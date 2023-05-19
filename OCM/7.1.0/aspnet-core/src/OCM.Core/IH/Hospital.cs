using Abp.Domain.Entities.Auditing;

namespace OCM.IH
{
    /// <summary>
    /// 医院信息
    /// </summary>
    public class Hospital : FullAuditedEntity<int>
    {
        /// <summary>
        /// 医院名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 医院描述
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// API访问Appid
        /// </summary>
        public string AppId { set; get; }
        /// <summary>
        /// API密钥
        /// </summary>
        public string AppSecurityKey { set; get; }

    }
}
