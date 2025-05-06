using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Infrastructure.Data.RepositoryImplementation
{
    public class VendorRepository: IVendorRepository
    {

        private readonly MagazineContext _db;
        public VendorRepository(MagazineContext db)
        {
            _db = db;
        }
        public async Task<ServiceResponse<Vendor>> AddVendorAsync(Vendor vendor)

        {     //  inspecting for existing vendor by company name
            var existingVendor = await _db.Vendors.FirstOrDefaultAsync(v => v.CompanyName == vendor.CompanyName);
            if (existingVendor != null)
                return new ServiceResponse<Vendor>(null!, success: false, message: "A vendor with this company name already exist.");
            // inspecting for duplicate email
            var existingVendorEmail = await _db.Vendors.FirstOrDefaultAsync(v => v.Email == vendor.Email);
            if (existingVendorEmail != null)
                return new ServiceResponse<Vendor>(null!, success: false, message: "This Email Address is already registered by a Vendor.");

            //  add vendor after validation
            await _db.Vendors.AddAsync(vendor);
            await _db.SaveChangesAsync();
            return new ServiceResponse<Vendor>(null!, success: true, message: "Vendor was added successfully!.");


        }

        public async Task<PagedResult<Vendor>> GetAllApprovedVendorsAsync(int pageNumber, int pageSize)
        {
            var query = _db.Vendors
                           .Where(v => v.IsApproved)
                           .Include(v => v.Products) // Include all related products
                           .AsQueryable();

            int totalCount = await query.CountAsync();

            var vendors = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // If you only want to return vendors with only their approved products (optional)
            foreach (var vendor in vendors)
            {
                vendor.Products = vendor.Products.Where(p => p.Vendor.IsApproved).ToList();
            }

            return new PagedResult<Vendor>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = vendors
            };
        }

        public async Task<Vendor> GetVendorByIdAsync(Guid vendorId)
        {
            return await _db.Vendors
                .Include(v => v.Products)
                .FirstOrDefaultAsync(v => v.Id == vendorId)
                ?? throw new KeyNotFoundException($"Vendor with ID {vendorId} was not found.");
        }


        public async Task UpdateVendorAsync(Vendor vendor)
        {
            _db.Vendors.Update(vendor);
            await _db.SaveChangesAsync();

        }

        public async Task<bool> HasActiveSubscriptionAsync(Guid vendorId)
        {
            return await _db.VendorSubscriptions
                .AnyAsync(vs => vs.VendorId == vendorId && vs.ExpiryDate >= DateTime.UtcNow);
        }

      

        public async Task SubscribeVendorAsync(Guid vendorId)
        {
            var vendor = await _db.Vendors.FindAsync(vendorId);
            if (vendor == null) throw new Exception("Vendor not found");

            vendor.HasActiveSubscription = true;
            vendor.SubscriptionStartDate = DateTime.UtcNow;
            vendor.SubscriptionEndDate = DateTime.UtcNow.AddDays(30); // 30 days subscription

            _db.Vendors.Update(vendor);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync() => (await _db.SaveChangesAsync()) > 0;

      

        //public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        //{
        //    // Directly use ToListAsync() on the DbSet
        //    return await _db.Vendors.ToListAsync();
        //}

        public  async Task DeleteVendorAsync(Vendor vendor)
        {
            //throw new NotImplementedException();
            var findVendor = await _db.Vendors.FindAsync(vendor.Id);
            if (findVendor != null)
            {
                _db.Vendors.Remove(vendor);
                await _db.SaveChangesAsync();
            }
        }

        //public Task<PagedResult<Vendor>> GetAllVendorsAsync(int pageNumber, int pageSize)
        //{
        //    // throw new NotImplementedException();
        //    var query = _db.Vendors
        //                  // .Where(v => v.IsApproved)
        //                   .Include(v => v.Products) // Include all related products
        //                   .AsQueryable();

        //    int totalCount = await query.CountAsync();

        //    var vendors = await query
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    // If you only want to return vendors with only their approved products (optional)
        //    foreach (var vendor in vendors)
        //    {
        //        vendor.Products = vendor.Products.Where(p => p.Vendor.IsApproved).ToList();
        //    }

        //    return new PagedResult<Vendor>
        //    {
        //        TotalCount = totalCount,
        //        PageSize = pageSize,
        //        PageNumber = pageNumber,
        //        Items = vendors
        //    };
        //}
    }
}
