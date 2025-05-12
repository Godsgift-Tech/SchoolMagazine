using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.Interface;

namespace SchoolMagazine.Infrastructure.Data.RepositoryImplementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly MagazineContext _db;

        public ProductRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task AddAsync(SchoolProduct product)
        {
            await _db.SchoolProducts.AddAsync(product);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<SchoolProduct>> GetPagedProductsAsync(string? name, string? category, Guid? vendorId, int pageNumber, int pageSize)
        {
            var query = _db.SchoolProducts.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.Contains(category));

            if (vendorId.HasValue)
                query = query.Where(p => p.VendorId == vendorId.Value);

            var skip = (pageNumber - 1) * pageSize;
            return await query.Skip(skip).Take(pageSize).ToListAsync();
        }


        public async Task DeleteAsync(SchoolProduct product)
        {
            _db.SchoolProducts.Remove(product);
            await _db.SaveChangesAsync();
        }


        public async Task<SchoolProduct> GetByProductIdAsync(SchoolProduct product)
        {
           
            return await _db.SchoolProducts.FindAsync(product.Id);
        }


        public async Task<SchoolVendor> GetVendorByIdAsync(Guid vendorId)
        {
            return await _db.SchoolVendors
                .Include(v => v.Products)
                .FirstOrDefaultAsync(v => v.Id == vendorId)
                ?? throw new KeyNotFoundException($"Vendor with ID {vendorId} was not found.");
        }



        public async Task UpdateAsync(SchoolProduct product)
        {
            _db.SchoolProducts.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(SchoolProduct product)
        {
            _db.SchoolProducts.Remove(product);
            await _db.SaveChangesAsync();
        }

       

        public Task<SchoolProduct> GetByProductIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<SchoolProduct> GetProductWithVendorAsync(Guid productId)
        {
            return await _db.SchoolProducts
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        //public Task DeleteProductAsync(SchoolVendor product)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
