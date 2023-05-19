using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace OCM.SYS.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMap(typeof(MenuItem))]
    public class MenuItemDto : FullAuditedEntityDto<int>
    {
        /// <summary>
        /// �ϼ��˵�ID
        /// </summary>
        [Required]
        public int ParentId { get; set; }
        /// <summary>
        /// �˵�����
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Lable { get; set; }
        /// <summary>
        /// �˵����
        /// </summary>
        [MaxLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// ǰ�˵�ַ
        /// </summary>
        [MaxLength(50)]
        public string Route { set; get; }
        /// <summary>
        /// ͼ��
        /// </summary>
        [MaxLength(50)]
        public string Icon { set; get; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        [MaxLength(50)]
        public string Description { set; get; }
        /// <summary>
        /// �˵����ࣺ1 ��̨ 2 ǰ��
        /// </summary>
        [MaxLength(2)]
        public string Classify { set; get; }
    }
}
