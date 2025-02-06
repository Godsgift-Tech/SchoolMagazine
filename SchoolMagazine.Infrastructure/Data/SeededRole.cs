using Microsoft.AspNetCore.Identity;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Infrastructure.Data
{
    public class SeededRole
    {
        //public static async Task SeedRolesAndUsers(RoleManager<Role> roleManager, UserManager<User> userManager)
        //{
        //    //// Seed roles if they don't already exist
        //    ////if (!await roleManager.RoleExistsAsync("Admin"))
        //    ////{
        //    ////    await roleManager.CreateAsync(new Role { Name = "Admin", NormalizedName = "ADMIN" });
        //    ////}

        //    //if (!await roleManager.RoleExistsAsync("School"))
        //    //{
        //    //    await roleManager.CreateAsync(new Role { Name = "School", NormalizedName = "SCHOOL" });
        //    //}

        //    //if (!await roleManager.RoleExistsAsync("Visitor"))
        //    //{
        //    //    await roleManager.CreateAsync(new Role { Name = "Visitor", NormalizedName = "VISITOR" });
        //    //}

        //    // Seed default users if they don't already exist
        //    var adminUser = await userManager.FindByEmailAsync("admin@schoolmagazine.com");
        //    if (adminUser == null)
        //    {
        //        adminUser = new User
        //        {
        //            UserName = "admin@schoolmagazine.com",
        //            Email = "admin@schoolmagazine.com",
        //            FirstName = "Admin",
        //            LastName = "User"
        //        };
        //        await userManager.CreateAsync(adminUser, "Admin@123");
        //        await userManager.AddToRoleAsync(adminUser, "Admin");
        //    }

        //    var schoolUser = await userManager.FindByEmailAsync("school@schoolmagazine.com");
        //    if (schoolUser == null)
        //    {
        //        schoolUser = new User
        //        {
        //            UserName = "school@schoolmagazine.com",
        //            Email = "school@schoolmagazine.com",
        //            FirstName = "School",
        //            LastName = "User"
        //        };
        //        await userManager.CreateAsync(schoolUser, "School@123");
        //        await userManager.AddToRoleAsync(schoolUser, "School");
        //    }

        //    var visitorUser = await userManager.FindByEmailAsync("visitor@schoolmagazine.com");
        //    if (visitorUser == null)
        //    {
        //        visitorUser = new User
        //        {
        //            UserName = "visitor@schoolmagazine.com",
        //            Email = "visitor@schoolmagazine.com",
        //            FirstName = "Visitor",
        //            LastName = "User"
        //        };
        //        await userManager.CreateAsync(visitorUser, "Visitor@123");
        //        await userManager.AddToRoleAsync(visitorUser, "Visitor");
        //    }
        //}
    }
}
