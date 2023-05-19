using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using OCM.EntityFrameworkCore;
using OCM.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace OCM.Web.Tests
{
    [DependsOn(
        typeof(OCMWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class OCMWebTestModule : AbpModule
    {
        public OCMWebTestModule(OCMEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(OCMWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(OCMWebMvcModule).Assembly);
        }
    }
}