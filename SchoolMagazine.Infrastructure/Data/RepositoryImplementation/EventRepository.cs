using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class EventRepository : IEventRepository
    {
        private readonly MagazineContext _db;
        public EventRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task AddEventsAsync(SchoolEvent schoolEvent)
        {
         _db.Events.Add(schoolEvent);
            await _db.SaveChangesAsync();  //saves event to database
        }

        public async Task<IEnumerable<SchoolEvent>> GetAllEventsAsync()
        {
          return await _db.Events.ToListAsync();
        }

        public async Task<SchoolEvent> GetEventsBySchool(School school)
        {
            return await _db.Events.FindAsync(school);
        }
    }


}
