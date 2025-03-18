using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IAdvertService
    {
        Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> GetAllAdvertsAsync(int pageNumber, int pageSize);
      
        Task<AdvertServiceResponse<SchoolAdvertDto>> PostAdvertAndPayAsync(SchoolAdvertDto advertDto, PaymentRequestDto paymentDetails);
      



    }



}
