using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Lee.EMS.Authorization.Roles;
using Lee.EMS.Authorization.Users;
using Lee.EMS.MultiTenancy;

namespace Lee.EMS.EntityFrameworkCore
{
    public class EMSDbContext : AbpZeroDbContext<Tenant, Role, User, EMSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public EMSDbContext(DbContextOptions<EMSDbContext> options)
            : base(options)
        {
        }
    }
}
