using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Infrastructure.Auth;
using SchoolMagazine.Infrastructure.Auto;
using System;
namespace SchoolMagazine.Infrastructure.Data
{
    public class MagazineContext : IdentityDbContext<User, Role, Guid>
    {
        public MagazineContext(DbContextOptions<MagazineContext> options) : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolEvent> Events { get; set; }
        public DbSet<SchoolAdvert> Adverts { get; set; }


       
    }


}