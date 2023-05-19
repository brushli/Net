using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCM.SYS.Dto
{
    [AutoMapTo(typeof(MenuItem))]
    public class CreateMenuItemDto
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
