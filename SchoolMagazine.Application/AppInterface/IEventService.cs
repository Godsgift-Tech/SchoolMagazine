using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IEventService
    {
        Task<IEnumerable<SchoolEventDto>> GetAllEventsAsync();
        Task<IEnumerable<SchoolEventDto>> GetEventsBySchool(string schoolName);

        Task<EventServiceResponse<SchoolEventDto>> AddSchoolEventsAsync(SchoolEventDto eventDetails);

        //Task UpdateSchoolEventAsync(SchoolEventDto eventDetails);
       // Task UpdateSchoolEventAsync(Guid id, SchoolEventDto schoolEvent);
        Task<EventServiceResponse<SchoolEventDto>> UpdateSchoolEventAsync(Guid id, SchoolEventDto schoolEvent);


        Task DeleteSchoolEventAsync(Guid eventId);



    }



       
    }
