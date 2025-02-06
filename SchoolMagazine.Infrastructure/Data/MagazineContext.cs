using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Infrastructure.Auto;

namespace SchoolMagazine.Infrastructure.Data
{
    public class MagazineContext : IdentityDbContext<User, Role, Guid>
    {
        public MagazineContext(DbContextOptions<MagazineContext> options) : base(options)
        {
        }

        // Add DbSet for additional entities
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolAdvert> Adverts { get; set; }
        public DbSet<SchoolEvent> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                // Set the table name for the User entity
                entity.ToTable("Users");

                // Configure properties
                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsRequired(); // FirstName is required

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsRequired(); // LastName is required

                // Indexes (optional)
                entity.HasIndex(e => e.Email).IsUnique(); // Ensure email is unique
                entity.HasIndex(e => e.UserName).IsUnique(); // Ensure username is unique

                // Relationships
                // If needed, configure relationships here
                // For example, if User has a one-to-many or many-to-many relationship with another entity
            });

            //        builder.Entity<School>()
            //.HasMany(sch => sch.Adverts) // A School can have many Advertisements
            //.WithOne(ad => ad.SchoolName) // Each Advertisement belongs to one School
            //.HasForeignKey(ad => ad.SchoolId) // Foreign key in the Advertisement table
            //.OnDelete(DeleteBehavior.Cascade); // Delete Advertisements if the School is deleted

            //        builder.Entity<School>()
            //.HasMany(sch => sch.Events) // A School can have many Events
            //.WithOne(evnt => evnt.SchoolName) // Each Event belongs to one School
            //.HasForeignKey(evnt => evnt.SchoolId) // Foreign key in the Event table
            //.OnDelete(DeleteBehavior.Cascade); // Delete Events if the School is deleted

            //

            //SeedRoles(builder);

            //var hasher = new PasswordHasher<User>();
            ////create a user
            //builder.Entity<User>().HasData(
            //   new User
            //   {
            //       Id = Guid.Parse("632100bf-80bf-4603-9997-d6a013964c4a"),
            //       UserName = "godsgiftoghenechohwo@gmail.com",
            //       NormalizedUserName = "GODSGIFTOGHENECHOHWO@GMAIL.COM",
            //       PasswordHash = hasher.HashPassword(null, "Pa$$w0rd123"),
            //       FirstName = "Admin",
            //       LastName = "Luxe",
            //       Email = "godsgiftoghenechohwo@gmail.com",
            //       NormalizedEmail = "GODSGIFTOGHENECHOHWO@GMAIL.COM",
            //       EmailConfirmed = true,
            //       SecurityStamp = Guid.NewGuid().ToString()

            //   }
            //   );
//            builder.Entity<UserRoles>().HasData(
//    new UserRoles { UserId = "user-guid-1", RoleId = "role-guid-1" },
//    new UserRoles { UserId = "user-guid-2", RoleId = "role-guid-2" }
//);
            // Assign admin role to the user we created
            //builder.Entity<UserRoles>().HasData(
            //    new UserRoles
            //    {
            //        RoleId = Guid.Parse("52ecd840-7b0b-4b1e-887e-95d8e63cc1cc"), // for admin
            //        UserId = Guid.Parse("632100bf-80bf-4603-9997-d6a013964c4a")
            //    },
            //    new UserRoles
            //    {
            //        RoleId = Guid.Parse("f4a9b4d4-6138-45e0-ba58-b1121ce64825"),  // for user
            //        UserId = Guid.Parse("632100bf-80bf-4603-9997-d6a013964c4a")
            //    },

            //   builder.Entity<UserRoles>().HasKey(ur => 
            //   new
            //{
            //ur.UserId,
            //ur.RoleId
            //})

            //);

        }


        //private void SeedRoles(ModelBuilder builder)
        //{
        //    // Ensure that Role inherits from IdentityRole<Guid>
        //    builder.Entity<Role>().HasData(
        //        new Role
        //        {
        //            Id = Guid.Parse("52ecd840-7b0b-4b1e-887e-95d8e63cc1cc"),
        //            Name = "Admin",
        //            NormalizedName = "ADMIN",
        //            ConcurrencyStamp = Guid.NewGuid().ToString()
        //            // .HasKey(ur => new { ur.UserId, ur.RoleId });
        //        },
        //        new Role
        //        {
        //            Id = Guid.Parse("f4a9b4d4-6138-45e0-ba58-b1121ce64825"),
        //            Name = "User",
        //            NormalizedName = "USER",
        //            ConcurrencyStamp = Guid.NewGuid().ToString()
        //        }
        //    );

        }
    }
