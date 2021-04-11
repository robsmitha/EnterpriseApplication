// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Entities;
using IdentityModel;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace IdentityServerAspNetIdentity
{
    public class SeedData
    {
        public static async Task EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //using (var serviceProvider = services.BuildServiceProvider())
            //{
            //    using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //    {
            //        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            //        context.Database.Migrate();

            //        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //        var alice = userMgr.FindByNameAsync("alice").Result;
            //        if (alice == null)
            //        {
            //            alice = new ApplicationUser
            //            {
            //                UserName = "alice",
            //                Email = "AliceSmith@email.com",
            //                EmailConfirmed = true,
            //            };
            //            var result = userMgr.CreateAsync(alice, "Pass123$").Result;
            //            if (!result.Succeeded)
            //            {
            //                throw new Exception(result.Errors.First().Description);
            //            }

            //            result = userMgr.AddClaimsAsync(alice, new Claim[]{
            //                new Claim(JwtClaimTypes.Name, "Alice Smith"),
            //                new Claim(JwtClaimTypes.GivenName, "Alice"),
            //                new Claim(JwtClaimTypes.FamilyName, "Smith"),
            //                new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
            //            }).Result;
            //            if (!result.Succeeded)
            //            {
            //                throw new Exception(result.Errors.First().Description);
            //            }
            //            Log.Debug("alice created");
            //        }
            //        else
            //        {
            //            Log.Debug("alice already exists");
            //        }

            //        var bob = userMgr.FindByNameAsync("bob").Result;
            //        if (bob == null)
            //        {
            //            bob = new ApplicationUser
            //            {
            //                UserName = "bob",
            //                Email = "BobSmith@email.com",
            //                EmailConfirmed = true
            //            };
            //            var result = userMgr.CreateAsync(bob, "Pass123$").Result;
            //            if (!result.Succeeded)
            //            {
            //                throw new Exception(result.Errors.First().Description);
            //            }

            //            result = userMgr.AddClaimsAsync(bob, new Claim[]{
            //                new Claim(JwtClaimTypes.Name, "Bob Smith"),
            //                new Claim(JwtClaimTypes.GivenName, "Bob"),
            //                new Claim(JwtClaimTypes.FamilyName, "Smith"),
            //                new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
            //                new Claim("location", "somewhere")
            //            }).Result;
            //            if (!result.Succeeded)
            //            {
            //                throw new Exception(result.Errors.First().Description);
            //            }
            //            Log.Debug("bob created");
            //        }
            //        else
            //        {
            //            Log.Debug("bob already exists");
            //        }
            //    }
            //}
            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var administratorRole = new IdentityRole("Administrator");
            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "admin", Email = "administrator@localhost" };
            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                var result = await userManager.CreateAsync(administrator, "Pass123$");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await userManager.AddClaimsAsync(administrator, new Claim[]{
                                new Claim(JwtClaimTypes.Id, administrator.Id),
                                new Claim(JwtClaimTypes.Name, "Admin"),
                                new Claim(JwtClaimTypes.GivenName, "Admin"),
                                new Claim(JwtClaimTypes.FamilyName, "Admin"),
                                new Claim(JwtClaimTypes.WebSite, "http://admin.com"),
                            });
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            if (!context.SystemConfigurations.Any())
            {
                var data = JsonConvert.DeserializeObject<List<SystemConfiguration>>(File.ReadAllText("Seed" + Path.DirectorySeparatorChar + "systemConfigurations.json"));
                context.SystemConfigurations.AddRange(data);
                await context.SaveChangesAsync();
            }
        }
    }
}
