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
        Task AddAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<IEnumerable<Product>> GetPagedProductsAsync(string? name, string? category,
            Guid? vendorId, int pageNumber, int pageSize);
        Task PurchaseProductAsync(Guid schoolAdminId, Guid productId, int quantity);
        Task AddPurchaseAsync(SchoolPurchaseProduct purchase);
        Task<Product> GetByProductIdAsync(Product product);

       // Task<Product> GetByVendorIdAsync(Guid vendorId);
        Task<Vendor> GetVendorByIdAsync(Guid vendorId);
        Task UpdateAsync(Product product);
        Task DeleteProductAsync(Vendor product);
        Task<Product> GetByProductIdAsync(Guid productId);
        //Task GetByProductIdAsync(Guid id);
    }
}
