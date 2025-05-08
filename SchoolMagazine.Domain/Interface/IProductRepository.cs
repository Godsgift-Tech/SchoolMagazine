using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.Entities.VendorEntities;

namespace SchoolMagazine.Domain.Interface
{
    public interface IProductRepository
    {
        Task AddAsync(SchoolProduct product);
        Task DeleteProductAsync(SchoolProduct product);
        Task<IEnumerable<SchoolProduct>> GetPagedProductsAsync(string? name, string? category,
            Guid? vendorId, int pageNumber, int pageSize);
        Task PurchaseProductAsync(Guid schoolAdminId, Guid productId, int quantity);
        Task AddPurchaseAsync(PurchaseProduct purchase);
        Task<SchoolProduct> GetByProductIdAsync(SchoolProduct product);

       // Task<Product> GetByVendorIdAsync(Guid vendorId);
        Task<SchoolVendor> GetVendorByIdAsync(Guid vendorId);
        Task UpdateAsync(SchoolProduct product);
        Task DeleteProductAsync(SchoolVendor product);
        Task<SchoolProduct> GetByProductIdAsync(Guid productId);
        //Task GetByProductIdAsync(Guid id);
    }
}
