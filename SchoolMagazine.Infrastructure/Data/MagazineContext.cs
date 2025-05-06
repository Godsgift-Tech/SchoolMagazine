using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.UserRoleInfo;

namespace SchoolMagazine.Infrastructure.Data
{
    public class MagazineContext : IdentityDbContext<User, Role, Guid>
    {
        public MagazineContext(DbContextOptions<MagazineContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolEvent> Events { get; set; }
        public DbSet<SchoolAdvert> Adverts { get; set; }
        public DbSet<SchoolAdvertMedia> SchoolAdvertMedias { get; set; }
        public DbSet<SchoolEventMedia> SchoolEventMedias { get; set; }
        public DbSet<VendorSubscription> VendorSubscriptions { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SchoolPurchase> SchoolPurchases { get; set; }
        public DbSet<SchoolPurchaseProduct> SchoolPurchaseProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Identity configuration

            // Unique school name
            modelBuilder.Entity<School>()
                .HasIndex(s => s.SchoolName)
                .IsUnique();

            // School → Adverts
            modelBuilder.Entity<School>()
                .HasMany(s => s.Adverts)
                .WithOne(a => a.School)
                .HasForeignKey(a => a.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);

            // School → Events
            modelBuilder.Entity<School>()
                .HasMany(s => s.Events)
                .WithOne(e => e.School)
                .HasForeignKey(e => e.SchoolId)
                .OnDelete(DeleteBehavior.Cascade);

            // SchoolEvent → MediaItems
            modelBuilder.Entity<SchoolEvent>()
                .HasMany(e => e.EventMediaItems)
                .WithOne(m => m.SchoolEvent)
                .HasForeignKey(m => m.SchoolEventId)
                .OnDelete(DeleteBehavior.Cascade);

            // SchoolAdvert → MediaItems
            modelBuilder.Entity<SchoolAdvert>()
                .HasMany(a => a.AdvertMediaItems)
                .WithOne(m => m.SchoolAdvert)
                .HasForeignKey(m => m.SchoolAdvertId)
                .OnDelete(DeleteBehavior.Cascade);

            // Store MediaType enums as strings
            modelBuilder.Entity<SchoolEventMedia>()
                .Property(m => m.MediaType)
                .HasConversion<string>();

            modelBuilder.Entity<SchoolAdvertMedia>()
                .Property(m => m.MediaType)
                .HasConversion<string>();


            // Vendor - Product relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Vendor)
                .WithMany(v => v.Products)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            // SchoolPurchaseProduct
            modelBuilder.Entity<SchoolPurchaseProduct>()
                .HasKey(spp => new { spp.SchoolPurchaseId, spp.ProductId });

            // SchoolPurchase - SchoolPurchaseProduct relationship
            modelBuilder.Entity<SchoolPurchaseProduct>()
                .HasOne(spp => spp.SchoolPurchase)
                .WithMany(sp => sp.SchoolPurchaseProducts)
                .HasForeignKey(spp => spp.SchoolPurchaseId);

            

            // Product and vendor relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Vendor)
                .WithMany(v => v.Products)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vendor>()
        .Property(v => v.AmountPaid)
        .HasColumnName("AmountPaid");

            modelBuilder.Entity<Vendor>()
                .Property(v => v.HasActiveSubscription)
                .HasColumnName("HasActiveSubscription");

            modelBuilder.Entity<Vendor>()
                .Property(v => v.SubscriptionEndDate)
                .HasColumnName("SubscriptionEndDate");

            modelBuilder.Entity<Vendor>()
                .Property(v => v.SubscriptionStartDate)
                .HasColumnName("SubscriptionStartDate");






        }
    }
}
