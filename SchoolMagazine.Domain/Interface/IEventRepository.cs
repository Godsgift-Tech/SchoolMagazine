using SchoolMagazine.Domain.Entities;
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
        Task<School> GetEventsBySchool(Guid id, SchoolEvent schoolEvent);
        Task AddSchoolEventsAsync(SchoolEvent schoolEvent);
        Task UpdateSchoolEventAsync(Guid id, SchoolEvent schoolEvent);
        Task DeleteSchoolEventAsync(Guid id, SchoolEvent schoolEvent);

    }
}
