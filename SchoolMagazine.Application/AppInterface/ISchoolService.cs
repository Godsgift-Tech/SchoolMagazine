using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface ISchoolService
    {
        Task<ServiceResponse<IEnumerable<CreateSchoolDto>>> GetAllSchoolAsync();
        Task<ServiceResponse<PagedResult<SchoolDto>>> GetPagedSchoolsAsync(int pageNumber, int pageSize);

        Task<ServiceResponse<CreateSchoolDto>> GetSchoolByIdAsync(Guid id);
        Task<ServiceResponse<CreateSchoolDto>> GetSchoolByNameAsync(string schoolName);

        Task<ServiceResponse<List<CreateSchoolDto>>> GetSchoolsByLocationAsync(string location);
        Task<ServiceResponse<List<CreateSchoolDto>>> GetSchoolsByFeesRangeAsync(decimal feesRange);
        Task<ServiceResponse<List<CreateSchoolDto>>> GetSchoolsByRatingAsync(double rating);
        Task<ServiceResponse<CreateSchoolDto>> AddSchoolAsync(CreateSchoolDto schoolDto);
        Task<ServiceResponse<CreateSchoolDto>> UpdateSchoolByIdAsync(Guid id, CreateSchoolDto schoolDto);
        Task<ServiceResponse<bool>> DeleteSchoolByIdAsync(Guid id);




    }



}
