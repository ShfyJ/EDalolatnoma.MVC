using Microsoft.AspNetCore.Identity;
using EDalolatnoma.MVC.Constants;
using System.Threading.Tasks;
using EDalolatnoma.MVC.Models;

namespace EDalolatnoma.MVC.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Администратор"));

        }
    }
}