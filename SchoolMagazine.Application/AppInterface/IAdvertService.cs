using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IAdvertService
    {
        Task<AdvertServiceResponse<IEnumerable<SchoolAdvertDto>>> GetAllAdvertsAsync();
        //Task<SchoolDto> GetAdvertBySchool(Guid id, SchoolAdvertDto schooladvertDto);
        //Task AddSchoolAdvertAsync(SchoolAdvertDto schooladvertDto);
        //Task UpdateSchoolAdvertAsync(Guid id, SchoolAdvertDto schooladvertDto);
        //Task DeleteSchoolAdvertAsync(Guid id, SchoolAdvertDto schooladvertDto);
        //
        // Task<AdvertServiceResponse<IEnumerable<SchoolAdvertDto>>> GetAllAdvertsAsync();
        Task<AdvertServiceResponse<SchoolAdvertDto>> PostAdvertAndPayAsync(SchoolAdvertDto advertDto, PaymentRequestDto paymentDetails);
       // Task<AdvertServiceResponse<string>> ProcessAdvertPayment(Guid advertId, decimal amount, string currency, string paymentMethod);
        // Task<SchoolAdvertDto> GetAdvertByIdAsync(Guid id);
       // Task<IEnumerable<SchoolAdvertDto>> GetAdvertBySchoolId(Guid schoolId);

       // Task UpdateSchoolAdvertAsync(SchoolAdvertDto advertDetails);




    }



}
