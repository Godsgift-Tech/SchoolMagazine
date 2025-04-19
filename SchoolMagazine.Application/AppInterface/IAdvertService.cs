using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IAdvertService
    {
        //public async Task<ServiceResponse<SchoolAdvertDto>> AddAdvertAsync(CreateAdvertDto advertDto)

        Task<ServiceResponse<CreateAdvertDto>> CreateAdvertAsync(CreateAdvertDto advertDto);
        Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> GetAllAdvertsAsync(int pageNumber, int pageSize);
        Task<ServiceResponse<SchoolAdvertDto>> GetAdvertByIdAsync(Guid advertId);
        Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> GetAdvertsBySchoolIdAsync(Guid schoolId, int pageNumber, int pageSize);
        Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> SearchAdvertsAsync(string keyword, int pageNumber, int pageSize);
        Task<ServiceResponse<string>> DeleteAdvertAsync(Guid advertId);


    }



}
