using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Lee.EMS.Authorization.Roles;
using Lee.EMS.Authorization.Users;
using Lee.EMS.MultiTenancy;
using Lee.EMS.BAS;
using Lee.EMS.SYS;

namespace Lee.EMS.EntityFrameworkCore
{
    public class EMSDbContext : AbpZeroDbContext<Tenant, Role, User, EMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        /// <summary>
        /// 系统菜单
        /// </summary>
        public virtual DbSet<Dictionary> BasDictionary { get; set; }
        public virtual DbSet<DictionaryDetail> BasDictionaryDetail { get; set; }
        public virtual DbSet<Action> SysAction { get; set; }
        public virtual DbSet<SysModule> SysModule { get; set; }
        public virtual DbSet<ModuleActionInRole> SysModuleActionInRole { get; set; }
        public EMSDbContext(DbContextOptions<EMSDbContext> options)
            : base(options)
        {
        }
    }
}
