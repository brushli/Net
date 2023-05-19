using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Extensions;
using OCM.Core.DBAttributes;

namespace OCM.Authorization.Users
{
    public class User : AbpUser<User>
    {
        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>()
            };

            user.SetNormalizedNames();

            return user;
        }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classify { set; get; }
        /// <summary>
        /// 上级用户
        /// </summary>
        public long? ParentId { set; get; }
        /// <summary>
        /// 积分
        /// </summary>
        [DecimalPrecision(10, 2)]
        public decimal Integral { set; get; }
        /// <summary>
        /// 就诊人数
        /// </summary>
        public int PatientCount { set; get; }
    }
}
