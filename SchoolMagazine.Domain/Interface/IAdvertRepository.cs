using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Domain.Interface
{
    public interface IAdvertRepository
    {
        
        
        Task<bool> SchoolExistsAsync(Guid schoolId);
        Task AddAdvertAsync(SchoolAdvert advert);
        Task<PagedResult<SchoolAdvert>> GetPagedAdvertsAsync(int pageNumber, int pageSize);
        Task<SchoolAdvert?> GetAdvertByIdAsync(Guid advertId);
        Task<PagedResult<SchoolAdvert>> GetPagedAdvertsBySchoolIdAsync(Guid schoolId, int pageNumber, int pageSize);
        Task<PagedResult<SchoolAdvert>> SearchPagedAdvertsAsync(string keyword, int pageNumber, int pageSize);
        Task<bool> DeleteAdvertAsync(Guid advertId);


    }
}
