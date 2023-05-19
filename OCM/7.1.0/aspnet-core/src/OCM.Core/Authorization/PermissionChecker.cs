using Abp.Authorization;
using OCM.Authorization.Roles;
using OCM.Authorization.Users;

namespace OCM.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
