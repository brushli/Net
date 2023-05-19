using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace OCM.Controllers
{
    public abstract class OCMControllerBase: AbpController
    {
        protected OCMControllerBase()
        {
            LocalizationSourceName = OCMConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
