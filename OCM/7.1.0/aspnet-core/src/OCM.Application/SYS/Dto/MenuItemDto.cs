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
        /// 上级菜单ID
        /// </summary>
        [Required]
        public int ParentId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Lable { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        [MaxLength(50)]
        public string Code { get; set; }
        /// <summary>
        /// 前端地址
        /// </summary>
        [MaxLength(50)]
        public string Route { set; get; }
        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(50)]
        public string Icon { set; get; }
        /// <summary>
        /// 描述信息
        /// </summary>
        [MaxLength(50)]
        public string Description { set; get; }
        /// <summary>
        /// 菜单分类：1 后台 2 前端
        /// </summary>
        [MaxLength(2)]
        public string Classify { set; get; }
    }
}
