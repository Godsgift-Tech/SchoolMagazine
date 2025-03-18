using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Interface
{
    public interface IEventRepository
    {
        Task<IEnumerable<SchoolEvent>> GetAllEventsAsync();
        Task<PagedResult<SchoolEvent>> GetEventsAsync(
        string? title,
        string? description,
        Guid? schoolId,
        string? schoolName,
        int pageNumber,
        int pageSize);

       // Task<IEnumerable<SchoolEvent>> GetEventsByName(string eventName);
       Task<IEnumerable<SchoolEvent>> GetEventsBySchoolAsync(string schoolName);

       Task<SchoolEvent?> GetEventByTitleAndDescription(string title, string description, Guid schoolId);

        Task<EventServiceResponse<SchoolEvent>> AddSchoolEventsAsync(SchoolEvent eventDetails);
        Task<bool> SchoolExistsAsync(Guid schoolId);
        Task<SchoolEvent?> GetEventByIdAsync(Guid id);

        Task<SchoolEvent?> GetEventByTitleAsync(string title, Guid schoolId);
        Task<IEnumerable<SchoolEvent>> GetEventsBySchoolId(Guid schoolId);


        Task UpdateSchoolEventAsync(SchoolEvent eventDetails);
        Task DeleteSchoolEventAsync(Guid eventId);
    }

}
