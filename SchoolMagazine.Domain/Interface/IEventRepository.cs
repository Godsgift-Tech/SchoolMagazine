using SchoolMagazine.Domain.Entities;
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
      
        Task<IEnumerable<SchoolEvent>> GetEventsBySchool(string schoolName);

        Task<EventServiceResponse<SchoolEvent>> AddSchoolEventsAsync(SchoolEvent eventDetails);
        Task<bool> SchoolExistsAsync(Guid schoolId);
        Task<SchoolEvent?> GetEventByIdAsync(Guid id);

        Task<SchoolEvent?> GetEventByTitleAsync(string title, Guid schoolId);

        Task UpdateSchoolEventAsync(SchoolEvent eventDetails);
        Task DeleteSchoolEventAsync(Guid eventId);
    }

}
