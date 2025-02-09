using EDalolatnoma.MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace EDalolatnoma.MVC
{
    public class Program
    {
public async static Task Main(string[] args)
{
    var host = CreateHostBuilder(args).Build();

    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("app");
        try
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
           
            await Seeds.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
            logger.LogInformation("Finished Seeding Default Data");
            logger.LogInformation("Application Starting");
                    CultureInfo invC = new CultureInfo("uz-Cyrl-UZ", false);//CultureInfo.InvariantCulture;
                    CultureInfo.DefaultThreadCurrentCulture = invC;
                    CultureInfo.DefaultThreadCurrentUICulture = invC;
                }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "An error occurred seeding the DB");
        }
    }
    host.Run();
}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}