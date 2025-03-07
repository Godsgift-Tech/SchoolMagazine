using SchoolMagazine.Application.AppService.Paged;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface ISchoolService
    {
        Task<ServiceResponse<IEnumerable<SchoolDto>>> GetAllSchoolAsync();
        Task<ServiceResponse<PagedResult<SchoolDto>>> GetPagedSchoolAsync(int pageNumber, int pageSize);


        Task<ServiceResponse<SchoolDto>> GetSchoolByIdAsync(Guid id);
        Task<ServiceResponse<SchoolDto>> GetSchoolByNameAsync(string schoolName);

        Task<ServiceResponse<List<SchoolDto>>> GetSchoolsByLocationAsync(string location);
        Task<ServiceResponse<List<SchoolDto>>> GetSchoolsByFeesRangeAsync(decimal feesRange);
        Task<ServiceResponse<List<SchoolDto>>> GetSchoolsByRatingAsync(double rating);
        //  Task<ServiceResponse<SchoolDto>> RateSchoolByIdAsync(Guid schoolId, double rating);
        Task<ServiceResponse<SchoolDto>> AddSchoolAsync(SchoolDto schoolDto);
       // Task<ServiceResponse<SchoolDto>> UpdateSchoolByIdAsync(Guid id, SchoolDto schoolDto);
        Task<ServiceResponse<SchoolDto>> UpdateSchoolByIdAsync(Guid id, SchoolDto schoolDto);
        Task<ServiceResponse<bool>> DeleteSchoolByIdAsync(Guid id);

        // Task <ServiceResponse<IEnumerable<SchoolDto>>>SchoolQueryAsync(SchoolSearchQuery schoolSearchQuery);



    }



}
