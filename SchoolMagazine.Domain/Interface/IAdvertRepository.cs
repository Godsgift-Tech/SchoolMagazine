using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Domain.Interface
{
    public interface IAdvertRepository
    {
        
        Task<PagedResult<SchoolAdvert>> GetAllAdvertsAsync(int pageNumber, int pageSize);
       
        Task<IEnumerable<SchoolAdvert>> GetAdvertBySchoolAsync(string schoolName);


        Task<AdvertServiceResponse<SchoolAdvert>> AddSchoolAdvertAsync(SchoolAdvert advertDetails);
        Task<bool> SchoolExistsAsync(Guid schoolId);
        Task<SchoolAdvert?> GetAdvertByIdAsync(Guid id);

        Task<SchoolAdvert?> GetAdvertByTitleAsync(string title, Guid schoolId);
        Task<IEnumerable<SchoolAdvert>> GetAdvertBySchoolId(Guid schoolId);

        Task<IEnumerable<SchoolAdvert>> GetAllPaidAdvertsAsync();
        Task UpdateSchoolAdvertAsync(SchoolAdvert advertDetails);
        Task DeleteSchoolAdvertAsync(Guid edvertId);

    }
}
