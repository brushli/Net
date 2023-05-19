using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using OCM.Authorization;
using OCM.MenuItems;

namespace OCM
{
    [DependsOn(
        typeof(OCMCoreModule),
        typeof(AbpAutoMapperModule))]
    public class OCMApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<OCMAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(OCMApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
            
        }
        public override void PostInitialize()
        {
            base.PostInitialize();
            try
            {
                var repository = IocManager.Resolve<MenuItemAppService>();
                repository.InitPermissions();
            }
            catch (System.Exception ex)
            {
                Logger.Error("初始化权限遇到错误:" + ex.Message);
            }
        }
    }
}
