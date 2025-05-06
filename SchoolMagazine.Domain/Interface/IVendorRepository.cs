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
        //Task<Vendor> GetByIdAsync(Guid id);
        //Task<IEnumerable<Vendor>> GetAllApprovedVendorsAsync();
        //Task AddAsync(Vendor vendor);
        //Task UpdateAsync(Vendor vendor);
        //Task<IEnumerable<Vendor>> GetVendorsWithActiveSubscriptionAsync();
        ////

        Task<ServiceResponse<Vendor>> AddVendorAsync(Vendor vendor);
        Task<PagedResult<Vendor>> GetAllApprovedVendorsAsync(int pageNumber, int pageSize);
        Task<Vendor> GetVendorByIdAsync(Guid vendorId);
        Task UpdateVendorAsync(Vendor vendor);
        Task DeleteVendorAsync(Vendor vendor);
        Task<bool> HasActiveSubscriptionAsync(Guid vendorId);
        Task SubscribeVendorAsync(Guid vendorId);
        Task<bool> SaveChangesAsync();
        //Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        //Task<PagedResult<Vendor>> GetAllVendorsAsync(int pageNumber, int pageSize);










    }
}
