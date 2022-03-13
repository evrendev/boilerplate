using EvrenDev.Application.Constants;
using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using EvrenDev.Application.Enums.Identity;

namespace EvrenDev.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager,
            List<Department> departments,
            ILoggerFactory loggerFactory)
        {
            var log = loggerFactory.CreateLogger<DefaultUsers>();
            var departmentId = departments.FirstOrDefault(d => string.Equals(d.Title, SeedSoftwareDepartmentInfo.DEFAULT_TITLE)).Id;

            var superUser = new ApplicationUser
            {
                UserName = SeedSuperAdminInfo.USERNAME,
                Email = SeedSuperAdminInfo.EMAIL,
                FirstName = SeedSuperAdminInfo.FIRSTNAME,
                LastName = SeedSuperAdminInfo.LASTNAME,
                DepartmentId = departmentId,
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