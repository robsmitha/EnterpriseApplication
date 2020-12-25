using Domain.Entities;
using Infrastructure.Settings;
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
