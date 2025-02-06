using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Domain.Entities;

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

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsRequired(); // Role is required

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()") // Default to the current UTC time
                    .ValueGeneratedOnAdd(); // Only set on creation

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()") // Default to the current UTC time
                    .ValueGeneratedOnAddOrUpdate(); // Update whenever the entity is modified

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true); // Default to true

                // Indexes (optional)
                entity.HasIndex(e => e.Email).IsUnique(); // Ensure email is unique
                entity.HasIndex(e => e.UserName).IsUnique(); // Ensure username is unique

                // Relationships
                // If needed, configure relationships here
                // For example, if User has a one-to-many or many-to-many relationship with another entity
            });
            // Customize Identity tables if necessary
            // Example: A User can be associated with multiple schools
         //->   builder.Entity<User>()
            //    .HasMany(user => user.Schools) // Define the navigation property
               // .WithOne(school => school.User) // Define the inverse navigation property
                //.HasForeignKey(school => school.UserId) // Specify the foreign key
                //.OnDelete(DeleteBehavior.Cascade); // Define the delete behavior

            builder.Entity<School>()
    .HasMany(sch => sch.Adverts) // A School can have many Advertisements
    .WithOne(ad => ad.SchoolName) // Each Advertisement belongs to one School
    .HasForeignKey(ad => ad.SchoolId) // Foreign key in the Advertisement table
    .OnDelete(DeleteBehavior.Cascade); // Delete Advertisements if the School is deleted

            builder.Entity<School>()
    .HasMany(sch => sch.Events) // A School can have many Events
    .WithOne(evnt => evnt.SchoolName) // Each Event belongs to one School
    .HasForeignKey(evnt => evnt.SchoolId) // Foreign key in the Event table
    .OnDelete(DeleteBehavior.Cascade); // Delete Events if the School is deleted



        }
    }
}