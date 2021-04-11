using Domain.Entities;
using Infrastructure.Identity;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }
        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            if (!context.SystemConfigurations.Any())
            {
                var data = JsonConvert.DeserializeObject<List<SystemConfiguration>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "systemConfigurations.json"));
                context.SystemConfigurations.AddRange(data);
                await context.SaveChangesAsync();
            }
        }
    }
}
