using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Application.DTOs;
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
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity is configured

            modelBuilder.Entity<School>()
                .HasIndex(s => s.SchoolName)
                .IsUnique(); // ✅ Ensures SchoolName is unique

            modelBuilder.Entity<School>()
    .HasMany(s => s.Adverts)
    .WithOne(a => a.School)
    .HasForeignKey(a => a.SchoolId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SchoolEvent>()
        .HasOne(e => e.School)
        .WithMany(s => s.Events)
        .HasForeignKey(e => e.SchoolId)
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SchoolAdvert>()
        .HasOne(e => e.School)
        .WithMany(s => s.Adverts)
        .HasForeignKey(e => e.SchoolId)
        .OnDelete(DeleteBehavior.Cascade);
        }



    }


}