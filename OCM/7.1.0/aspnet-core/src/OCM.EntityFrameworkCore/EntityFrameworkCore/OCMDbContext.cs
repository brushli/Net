using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using OCM.Authorization.Roles;
using OCM.Authorization.Users;
using OCM.MultiTenancy;
using System.Linq;
using OCM.Core.DBAttributes;
using System.Reflection;
using OCM.SYS;
using OCM.IH;

namespace OCM.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public class OCMDbContext : AbpZeroDbContext<Tenant, Role, User, OCMDbContext>
    {
        /// <summary>
        /// 系统菜单
        /// </summary>
        public virtual DbSet<MenuItem> sys_MenuItems { get; set; }
        /// <summary>
        /// 菜单与角色
        /// </summary>
        public virtual DbSet<MenuItemInRole> sys_MenuItemInRole { get; set; }
        /// <summary>
        /// 医院
        /// </summary>
        public virtual DbSet<Hospital> Hospital { get; set; }
        /// <summary>
        /// 患者
        /// </summary>
        public virtual DbSet<Patient> Patient { get; set; }
        public OCMDbContext(DbContextOptions<OCMDbContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// 模型创建前
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("sys_");
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                var type = item.ClrType;
                var props = type.GetProperties().Where(c => c.IsDefined(typeof(DecimalPrecisionAttribute), true)).ToArray();
                foreach (var p in props)
                {
                    var precis = p.GetCustomAttribute<DecimalPrecisionAttribute>();
                    modelBuilder.Entity(type).Property(p.Name).HasColumnType($"decimal({precis.Precision},{precis.Scale})");
                }
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
