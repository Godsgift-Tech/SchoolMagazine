using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class EventRepository : IEventRepository
    {
        private readonly MagazineContext _db;
        public EventRepository(MagazineContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<SchoolEvent>> AddEventsAsync(SchoolEvent schoolEvent)
        {
            _db.Events.Add(schoolEvent);
            await _db.SaveChangesAsync();
            return _db.Events;
        }

        public async Task<IEnumerable<SchoolEvent>> GetAllEventsAsync()
        {
          return await _db.Events.ToListAsync();

        }

        public async Task<IEnumerable<SchoolEvent>> GetEventsByIdAsync(Guid id)
        {
            return (IEnumerable<SchoolEvent>)await _db.Events.FindAsync(id);

        }
    }
}
