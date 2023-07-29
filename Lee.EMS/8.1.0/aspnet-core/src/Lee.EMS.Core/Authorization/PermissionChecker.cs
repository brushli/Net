using Abp.Authorization;
using Lee.EMS.Authorization.Roles;
using Lee.EMS.Authorization.Users;

namespace Lee.EMS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
