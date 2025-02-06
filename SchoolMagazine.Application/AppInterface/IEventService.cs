using SchoolMagazine.Application.DTOs;

namespace SchoolMagazine.Application.AppInterface
{
    public interface IEventService
    {
        Task<IEnumerable<SchoolEventDto>> GetAllEventsAsync();
        Task<SchoolDto> GetEventsBySchool(Guid id, SchoolEventDto schoolEventDto);
        Task AddSchoolEventsAsync(SchoolEventDto schoolEventDto);
        Task UpdateSchoolEventAsync(Guid id, SchoolEventDto schoolEventDto);
        Task DeleteSchoolEventAsync(Guid id, SchoolEventDto schoolEventDto);

    }



}
