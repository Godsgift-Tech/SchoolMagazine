using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.Paging;

namespace SchoolMagazine.Domain.Interface
{
    public interface IProductRepository
    {
        Task AddAsync(SchoolProduct product);
        Task DeleteProductAsync(SchoolProduct product);

        Task<PagedResult<SchoolProduct>> GetPagedProductsAsync(
    string? name,
    string? category,
    Guid? vendorId,
    int pageNumber,
    int pageSize);
        Task<SchoolProduct> GetByProductIdAsync(SchoolProduct product);

        Task<SchoolVendor> GetVendorByIdAsync(Guid vendorId);
        Task UpdateAsync(SchoolProduct product);
        Task<SchoolProduct> GetProductWithVendorAsync(Guid productId);

        Task<SchoolProduct> GetByProductIdAsync(Guid productId);
    }
}
