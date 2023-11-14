using Microsoft.AspNetCore.Authorization;

namespace EDalolatnoma.MVC.Permission
{
internal class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; private set; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
}