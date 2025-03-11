using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Domain.Interface
{
    public interface IAdvertRepository
    {
        Task<IEnumerable<SchoolAdvert>> GetAllAdvertsAsync();
     

       // Task<IEnumerable<SchoolEvent>> GetAllEventsAsync();

     //   Task<IEnumerable<SchoolAdvert>> GetAdvertByName(string eventName);
        Task<IEnumerable<SchoolAdvert>> GetAdvertBySchoolAsync(string schoolName);

       // Task<SchoolEvent?> GetAdvertByTitleAndDescription(string title, string description, Guid schoolId);

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
