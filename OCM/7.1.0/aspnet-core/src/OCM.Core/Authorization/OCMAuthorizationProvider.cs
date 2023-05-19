using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;
using OCM.SYS;
using System.Collections.Generic;

namespace OCM.Authorization
{
    public class OCMAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"), L("Users"),properties: new Dictionary<string, object>() {
                    {"tag", new MenuItem{Lable= (L("Users") as LocalizableString).Name,Route="/app/users",Icon="fas fa-users"} } });
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"), L("Roles"), properties: new Dictionary<string, object>() {
                    {"tag", new MenuItem{Lable= (L("Roles") as LocalizableString).Name,Route ="/app/roles",Icon="fas fa-theater-masks"}} });
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), L("Tenants"), properties: new Dictionary<string, object>() {
                    {"tag", new MenuItem{Lable= (L("Tenants") as LocalizableString).Name,Route ="/app/tenants",Icon="fas fa-building"}} }, multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, OCMConsts.LocalizationSourceName);
        }
    }
}
