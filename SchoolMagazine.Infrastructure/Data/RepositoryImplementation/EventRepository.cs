using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using System.Data.Entity;

namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class EventRepository : IEventRepository
    {
        private readonly MagazineContext _db;
        public EventRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task AddSchoolEventsAsync(SchoolEvent schoolEvent)
        {
         _db.Events.Add(schoolEvent);
            await _db.SaveChangesAsync();  //saves event to database
        }

        public async Task UpdateSchoolEventAsync(Guid id, SchoolEvent schoolEvent)
        {


            var events = await _db.Events.FindAsync(id);

            if (events == null) throw new Exception("Event was not found");


           
            if (schoolEvent == null)
            {
                throw new Exception("Event was not found");
            }

            _db.Events.Update(events);
            await _db.SaveChangesAsync();


        }
        public async Task DeleteSchoolEventAsync(Guid id, SchoolEvent schoolEvent)
        {


            var events = await _db.Events.FindAsync(id);

            if (events == null) throw new Exception("Event was not found");


            if (schoolEvent == null)
            {
                throw new Exception("Event was not found");
            }

            _db.Events.Remove(events);
            await _db.SaveChangesAsync();


        }


        //
        public async Task<IEnumerable<SchoolEvent>> GetAllEventsAsync()
        {
          return await _db.Events.ToListAsync();
        }

        public async Task<School> GetEventsBySchool(Guid id, SchoolEvent schoolEvent)
        {
            if (schoolEvent.SchoolName == null) throw new Exception("There was no event posted for this school");

            //  return _db.Events.FindAsync(await _db.Schools.FindAsync(schoolevent));
            // return await _db.Schools.FindAsync(_db.Events.FindAsync(schoolEvent.Id));
            
            return await _db.Schools.FindAsync(schoolEvent);

        

        }
    }


}
