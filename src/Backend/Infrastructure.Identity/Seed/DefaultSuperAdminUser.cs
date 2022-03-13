using EvrenDev.Application.Constants;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EvrenDev.Application.Enums.Identity;
using EvrenDev.Infrastructure.Identity.Model;

namespace EvrenDev.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            var log = loggerFactory.CreateLogger<DefaultUsers>();

            var superUser = new ApplicationUser
            {
                UserName = SeedSuperAdminInfo.USERNAME,
                Email = SeedSuperAdminInfo.EMAIL,
                FirstName = SeedSuperAdminInfo.FIRSTNAME,
                LastName = SeedSuperAdminInfo.LASTNAME,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                Deleted = false
            };

            var user = await userManager.FindByEmailAsync(superUser.Email);
                
            log.LogInformation("Super Admin user is created successfuly");

            if (user == null)
            {
                await userManager.CreateAsync(superUser, SeedSuperAdminInfo.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(superUser, UserRoles.SuperUser.Name);
                
                log.LogInformation("Super Admin user assigned all roles successfuly");
            }

            var modules = new string[] { 
                "Dashboard", 
                "Settings.Department", 
                "Settings.User",
                "Content"
            };
            
            await roleManager.SeedClaimsForRole(UserRoles.SuperUser.Name, modules);
        }
    }
}