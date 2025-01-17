
using Microsoft.AspNetCore.Identity;
using SchoolMagazine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Infrastructure.Data
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "School", "Visitor" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new Exception($"Failed to create role {role}: {errors}");

                      //  throw new Exception($"Failed to create role {role}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                   // foreach (var error in result.Errors)
                   // {
                    //    Console.WriteLine($"Error Object Type: {error.GetType()}");
                    //    Console.WriteLine($"Error Data: {error}");
                   // }
                }
            }
           
        }
    }
}
