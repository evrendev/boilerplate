using EvrenDev.Application.Constants;
using EvrenDev.Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EvrenDev.Infrastructure.Identity.Seeds
{
    public static class PermissionSeed
    {
        public static async Task AddPermissionClaimForRole(this RoleManager<ApplicationRole> roleManager, 
            ApplicationRole role, 
            string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                }
            }
        }

        public async static Task SeedClaimsForRole(this 
            RoleManager<ApplicationRole> roleManager,
            string roleName,
            string[] modules)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            foreach(var module in modules)
            {
                await roleManager.AddPermissionClaimForRole(role, module);
            }
        }
    }
}