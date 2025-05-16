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
    public interface IPurchaseService
    {
        Task<PurchaseProductResponseDto> CalculatePurchaseAsync(PurchaseProductRequestDto request);
        Task<ServiceResponse<PagedResult<SchoolProductDto>>> GetAllProductAsync(string? name, string? category, Guid? vendorId, int pageNumber, int pageSize);

    }
}
