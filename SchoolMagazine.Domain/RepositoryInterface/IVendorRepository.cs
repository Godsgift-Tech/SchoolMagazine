using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Domain.Interface
{
    public interface IVendorRepository
    {
       

        Task<ServiceResponse<SchoolVendor>> AddVendorAsync(SchoolVendor vendor);
        Task<PagedResult<SchoolVendor>> GetAllApprovedVendorsAsync(int pageNumber, int pageSize);
        Task<SchoolVendor> GetVendorByIdAsync(Guid vendorId);
        Task<SchoolProduct?> GetProductByIdAsync(Guid productId);
        Task UpdateProductAsync(SchoolProduct product);
        Task UpdateVendorAsync(SchoolVendor vendor);
        Task DeleteVendorAsync(SchoolVendor vendor);
        Task<bool> HasActiveSubscriptionAsync(Guid vendorId);
        Task SubscribeVendorAsync(Guid vendorId);
        Task<bool> SaveChangesAsync();
        //Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        //Task<PagedResult<Vendor>> GetAllVendorsAsync(int pageNumber, int pageSize);










    }
}
