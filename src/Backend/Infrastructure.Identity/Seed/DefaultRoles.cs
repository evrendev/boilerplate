using EvrenDev.Application.Enums.Identity;
using EvrenDev.Infrastructure.Identity.Model;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EvrenDev.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            foreach (var userRole in UserRoles.List())
            {
                await roleManager.CreateAsync(new ApplicationRole(){
                    Name = userRole.Name
                });
            }
        }
    }
}