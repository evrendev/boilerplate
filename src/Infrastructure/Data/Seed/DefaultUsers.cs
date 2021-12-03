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
    public class DefaultUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager,
            List<Department> departments,
            ILoggerFactory loggerFactory)
        {
            var log = loggerFactory.CreateLogger<DefaultUsers>();

            var managementDepartmentId = departments.FirstOrDefault(d => string.Equals(d.Title, SeedAdminisrationDepartmentInfo.DEFAULT_TITLE)).Id;
            var financeDepartmentId = departments.FirstOrDefault(d => string.Equals(d.Title, SeedEditorialDepartmentInfo.DEFAULT_TITLE)).Id;
            var basicUserDepartmentId = departments.FirstOrDefault(d => string.Equals(d.Title, SeedBasicUserDepartmentInfo.DEFAULT_TITLE)).Id;
            
            var editorialDepartmentUser = new ApplicationUser
            {
                UserName = SeedEditorialDepartmentUserInfo.USERNAME,
                Email = SeedEditorialDepartmentUserInfo.EMAIL,
                FirstName = SeedEditorialDepartmentUserInfo.FIRSTNAME,
                LastName = SeedEditorialDepartmentUserInfo.LASTNAME,
                DepartmentId = financeDepartmentId,
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
                DepartmentId = basicUserDepartmentId,
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
                DepartmentId = managementDepartmentId,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                Deleted = false
            };

            var basicUser = await userManager.FindByEmailAsync(basicDepartmentUser.Email);
            var editorUser = await userManager.FindByEmailAsync(editorialDepartmentUser.Email);
            var adminUser = await userManager.FindByEmailAsync(administationDepartmentUser.Email);
            
            if (basicUser == null)
            {
                await userManager.CreateAsync(basicDepartmentUser, SeedBasicUserDepartmentUserInfo.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(basicDepartmentUser, UserRoles.BasicUser.Value);
                    
                log.LogInformation("Editor is created successfuly");
            }
            
            if (editorUser == null)
            {
                await userManager.CreateAsync(editorialDepartmentUser, SeedEditorialDepartmentUserInfo.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(editorialDepartmentUser, UserRoles.Editor.Value);
                    
                log.LogInformation("Editor is created successfuly");
            }

            if (adminUser == null)
            {
                await userManager.CreateAsync(administationDepartmentUser, SeedAdministrationDepartmentUserInfo.DEFAULT_PASSWORD);
                await userManager.AddToRoleAsync(administationDepartmentUser, UserRoles.Administrator.Value);
                    
                log.LogInformation("Administrator is created successfuly");
            }

            if(adminUser != null) {
                var modules = new string[] { 
                    "Dashboard", 
                    "Settings.Department", 
                    "Settings.User",
                    "Content"
                };
                
                await roleManager.SeedClaimsForRole(UserRoles.Administrator.Value, modules);
                    
                log.LogInformation("Administration department user permission is created successfuly");
            }

            if(editorUser != null) {
                var modules = new string[] { 
                    "Dashboard",
                    "Content"
                };
                
                await roleManager.SeedClaimsForRole(UserRoles.Editor.Value, modules);
                    
                log.LogInformation("Editorial department user permission is created successfuly");
            }

            if(basicUser != null) {
                var modules = new string[] { };
                
                await roleManager.SeedClaimsForRole(UserRoles.BasicUser.Value, modules);
                    
                log.LogInformation("Basic user permission is created successfuly");
            }
        }
    }
}