using EDalolatnoma.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace EDalolatnoma.MVC.Permission
{
internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
            if (context.User == null)
            {
                return;
            }
            var user = await _userManager.GetUserAsync(context.User);
            if (user != null)
            {
                var userRoleNames = await _userManager.GetRolesAsync(user);

                var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));


            }
            var permissionss = context.User.Claims.Where(x => x.Type == "Permission" &&
                                                            x.Value == requirement.Permission &&
                                                            x.Issuer == "LOCAL AUTHORITY");
            if (permissionss.Any())
            {
                context.Succeed(requirement);
                return;
            }
        }
}
}