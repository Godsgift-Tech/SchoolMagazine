using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IEventService
    {
        Task<ServiceResponse<PagedResult<SchoolEventDto>>> GetAllEventsAsync(int pageNumber, int pageSize);

        Task<ServiceResponse<PagedResult<SchoolEventDto>>> GetEventsAsync(
         string? title, string? description, Guid? schoolId, string? schoolName, int pageNumber, int pageSize);
        Task<IEnumerable<SchoolEventDto>> GetEventsBySchoolAsync(string schoolName);
        Task<EventServiceResponse<SchoolEventDto>> AddSchoolEventsAsync(SchoolEventDto eventDetails);
        Task<EventServiceResponse<IEnumerable<SchoolEventDto>>> GetEventsBySchool(Guid schoolId);

        Task<EventServiceResponse<SchoolEventDto>> UpdateSchoolEventAsync(Guid id, SchoolEventDto schoolEvent);
        Task DeleteSchoolEventAsync(Guid eventId);



    }



       
    }
