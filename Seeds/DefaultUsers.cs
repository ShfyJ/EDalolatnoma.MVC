using Microsoft.AspNetCore.Identity;
using EDalolatnoma.MVC.Constants;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EDalolatnoma.MVC.Models;

namespace EDalolatnoma.MVC.Seeds
{
    public static class DefaultUsers
    {
       

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@ung.uz",
                Email = "admin@ung.uz",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FioName = "Департамент ИКТ",
                Position =  "Разработчик",
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    
                    await userManager.AddToRoleAsync(defaultUser, "Администратор");
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Администратор");
            
/*            await roleManager.AddPermissionClaim(adminRole, Permissions.UsersList.View);
            await roleManager.AddPermissionClaim(adminRole, Permissions.UsersList.Create);
            await roleManager.AddPermissionClaim(adminRole, Permissions.UsersList.Edit);
            await roleManager.AddPermissionClaim(adminRole, Permissions.UsersList.Delete);

            await roleManager.AddPermissionClaim(adminRole, Permissions.RolesList.View);
            await roleManager.AddPermissionClaim(adminRole, Permissions.RolesList.Create);
            await roleManager.AddPermissionClaim(adminRole, Permissions.RolesList.Edit);
            await roleManager.AddPermissionClaim(adminRole, Permissions.RolesList.Delete);*/

            await roleManager.AddPermissionClaim(adminRole, Permissions.Company.View);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Company.Create);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Company.Edit);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Company.Delete);

            await roleManager.AddPermissionClaim(adminRole, Permissions.Fields.View);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Fields.Create);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Fields.Edit);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Fields.Delete);

            await roleManager.AddPermissionClaim(adminRole, Permissions.Wells.View);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Wells.Create);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Wells.Edit);
            await roleManager.AddPermissionClaim(adminRole, Permissions.Wells.Delete);

            await roleManager.AddPermissionClaim(adminRole, Permissions.WellStatus.View);
            await roleManager.AddPermissionClaim(adminRole, Permissions.WellStatus.Create);
            await roleManager.AddPermissionClaim(adminRole, Permissions.WellStatus.Edit);
            await roleManager.AddPermissionClaim(adminRole, Permissions.WellStatus.Delete);

            await roleManager.AddPermissionClaim(adminRole, Permissions.KernoDb.View);
            await roleManager.AddPermissionClaim(adminRole, Permissions.KernoDb.Create);
            await roleManager.AddPermissionClaim(adminRole, Permissions.KernoDb.Edit);
            await roleManager.AddPermissionClaim(adminRole, Permissions.KernoDb.Delete);

            await roleManager.AddPermissionClaim(adminRole, Permissions.PermissionList.View);
        }

        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string permission) 
        {
                var allClaims = await roleManager.GetClaimsAsync(role);
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
        }
    }
}