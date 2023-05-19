using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OCM.Core.Authorization.Users
{
    /// <summary>
    /// 第三方用户信息表
    /// </summary>
    public class UserSocial : FullAuditedEntity<long>
    {
        public int UserId { set; get; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Provider { set; get; }
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { set; get; }
        /// <summary>
        /// 平台关联ID
        /// </summary>
        public string UnionId { set; get; }
        /// <summary>
        /// 微信用户的OPENID
        /// </summary>
        public string OpenId { set; get; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { set; get; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public string Sex { set; get; }
        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        public string Province { set; get; }
        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        public string City { set; get; }
        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        public string Country { set; get; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string HeadImgUrl { set; get; }
        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        public string Privilege { set; get; }
        /// <summary>
        /// 上级用户ID
        /// </summary>
        public long? ParentUserId { set; get; }
    }
    
}
