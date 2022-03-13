using EvrenDev.Application.Constants;
using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using EvrenDev.Application.Enums.Identity;
using EvrenDev.Infrastructure.Model;

namespace EvrenDev.Infrastructure.Identity.Seeds
{
    public class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            var log = loggerFactory.CreateLogger<DefaultUsers>();
            
            var moderatorialDepartmentUser = new ApplicationUser
            {
                UserName = SeedModeratorialDepartmentUserInfo.USERNAME,
                Email = SeedModeratorialDepartmentUserInfo.EMAIL,
                FirstName = SeedModeratorialDepartmentUserInfo.FIRSTNAME,
                LastName = SeedModeratorialDepartmentUserInfo.LASTNAME,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                Deleted = false
            };

            var basicDepartmentUser = new ApplicationUser
            {
                UserName = SeedBasicUserDepartmentUserInfo.USERNAME,
                Email = SeedBasicUserDepartmentUserInfo.EMAIL,
                FirstName = SeedBasicUserDepartmentUserInfo.FIRSTNAME,
                LastName = SeedBasicUserDepartmentUserInfo.LASTNAME,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                Deleted = false
            };

            var administationDepartmentUser = new ApplicationUser
            {
                UserName = SeedAdministrationDepartmentUserInfo.USERNAME,
                Email = SeedAdministrationDepartmentUserInfo.EMAIL,
                FirstName = SeedAdministrationDepartmentUserInfo.FIRSTNAME,
                LastName = SeedAdministrationDepartmentUserInfo.LASTNAME,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                Deleted = false
            };

            var basicUser = await userManager.FindByEmailAsync(basicDepartmentUser.Email);
            var moderatorUser = await userManager.FindByEmailAsync(moderatorialDepartmentUser.Email);
            var adminUser = await userManager.FindByEmailAsync(administationDepartmentUser.Email);
            
            if (basicUser == null)
            {
                await userManager.CreateAsync(basicDepartmentUser, SeedBasicUserDepartmentUserInfo.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(basicDepartmentUser, UserRoles.BasicUser.Name);
                    
                log.LogInformation("Basic user is created successfuly");
            }
            
            if (moderatorUser == null)
            {
                await userManager.CreateAsync(moderatorialDepartmentUser, SeedModeratorialDepartmentUserInfo.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(moderatorialDepartmentUser, UserRoles.Moderator.Name);
                    
                log.LogInformation("Moderator user is created successfuly");
            }

            if (adminUser == null)
            {
                await userManager.CreateAsync(administationDepartmentUser, SeedAdministrationDepartmentUserInfo.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(administationDepartmentUser, UserRoles.Administrator.Name);
                    
                log.LogInformation("Admin user is created successfuly");
            }

            if(adminUser != null) {
                var modules = new string[] { 
                    "Dashboard", 
                    "Settings.Department", 
                    "Settings.User",
                    "Content"
                };
                
                await roleManager.SeedClaimsForRole(UserRoles.Administrator.Name, modules);
                    
                log.LogInformation("Admin user permission is created successfuly");
            }

            if(moderatorUser != null) {
                var modules = new string[] { 
                    "Dashboard",
                    "Content"
                };
                
                await roleManager.SeedClaimsForRole(UserRoles.Moderator.Name, modules);
                    
                log.LogInformation("Moderator user permission is created successfuly");
            }

            if(basicUser != null) {
                var modules = new string[] { };
                
                await roleManager.SeedClaimsForRole(UserRoles.BasicUser.Name, modules);
                    
                log.LogInformation("Basic user permission is created successfuly");
            }
        }
    }
}