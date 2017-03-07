using Abp.Authorization;
using DisasterReport.Authorization.Roles;
using DisasterReport.MultiTenancy;
using DisasterReport.Users;

namespace DisasterReport.Authorization
{
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
