using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Lee.EMS.Configuration;

namespace Lee.EMS.Web.Host.Startup
{
    [DependsOn(
       typeof(EMSWebCoreModule))]
    public class EMSWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public EMSWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EMSWebHostModule).GetAssembly());
        }
    }
}
