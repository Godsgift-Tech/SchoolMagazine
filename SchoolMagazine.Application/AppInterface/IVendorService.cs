using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IVendorService
    {
        Task<ServiceResponse<VendorDto>> AddVendorAsync(VendorDto vendor);
        Task<ServiceResponse<SchoolVendor>> ApproveVendorAsync(Guid vendorId);
        Task CreateProductAsync(Guid vendorId, SchoolProduct product);
        Task <ServiceResponse<CreateProductDto>>CreateProductAsync(Guid vendorId, CreateProductDto product);
        Task DeleteProductAsync(Guid productId);
        Task<PagedResult<SchoolVendor>> GetAllApprovedVendorsAsync(int pageNumber, int pageSize);
      
        Task<bool> HasActiveSubscriptionAsync(Guid vendorId);
        Task<ServiceResponse<string>> SubscribeVendorAsync(SubscriptionRequestDto request);

        Task SubscribeVendorAsync(Guid vendorId);
        Task<SchoolVendor> GetVendorByIdAsync(Guid vendorId);
        Task DeleteVendorAsync(Guid vendorId);
    }
}
