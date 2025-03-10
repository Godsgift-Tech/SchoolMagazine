using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Service_Response;
//using System.Data.Entity;



namespace SchoolMagazine.Infrastructure.Data.Service
{
    public class EventRepository : IEventRepository
    {
        private readonly MagazineContext _db;
        public EventRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<SchoolEvent>> GetAllEventsAsync()
        {
            return await _db.Events.ToListAsync();
            // return await _db.Schools.ToListAsync();

        }

        public async Task<SchoolEvent?> GetEventByTitleAndDescription(string title, string description, Guid schoolId)
        {
            return await _db.Events
                .FirstOrDefaultAsync(e => e.Title == title && e.Description == description && e.SchoolId == schoolId);
        }

        public async Task<SchoolEvent?> GetEventByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid event ID.");
            }
            return await _db.Events
                .Include(e => e.School) // Ensure related for navigation
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<SchoolEvent>> GetEventsBySchoolAsync(string schoolName)
        {
            return await _db.Events
                .Include(e => e.School)  // Include School entity
                .Where(e => e.School.SchoolName == schoolName) // Filter by SchoolName
                .ToListAsync();
        }
       



        public async Task<IEnumerable<SchoolEvent>> GetEventsByName(string eventlName)
        {


            return await _db.Events
           
        .Include(e => e.Title)  // Ensure School is a navigation property
         .Where(e => e.Title== eventlName)  // Ensure SchoolName exists
         .AsNoTracking()  // Optional for performance
         .ToListAsync();
        }
        public async Task<IEnumerable<SchoolEvent>> GetEventsBySchoolId(Guid schoolId)
        {
            return await _db.Events
               .Include(e => e.School) // ✅ Load related school data
                .Where(e => e.SchoolId == schoolId) // 🔍 Filter events by school ID
                .ToListAsync();
        }


        public async Task<EventServiceResponse<SchoolEvent>> AddSchoolEventsAsync(SchoolEvent eventDetails)
        {
            var response = new EventServiceResponse<SchoolEvent>();


            try
            {
                // Check if the school exists
                var schoolExists = await _db.Schools.AnyAsync(s => s.Id == eventDetails.SchoolId);
                if (!schoolExists)
                {
                    response.Success = false;
                    response.Message = "School not found.";
                    return response;
                }

                // Add event to the database
                await _db.Events.AddAsync(eventDetails);
                await _db.SaveChangesAsync();

                response.Data = eventDetails;
                response.Success = true;
                response.Message = "Event added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }


        public async Task UpdateSchoolEventAsync(SchoolEvent eventDetails)
        {
            var existingEvent = await _db.Events.FindAsync(eventDetails.Id);
            if (existingEvent != null)
            {
                existingEvent.Title = eventDetails.Title;
                existingEvent.Description = eventDetails.Description;
                existingEvent.EventDate = eventDetails.EventDate;
                existingEvent.SchoolId = eventDetails.SchoolId;
                existingEvent.MediaUrl = eventDetails.MediaUrl;

                _db.Events.Update(existingEvent);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteSchoolEventAsync(Guid eventId)
        {
            var existingEvent = await _db.Events.FindAsync(eventId);
            if (existingEvent != null)
            {
                _db.Events.Remove(existingEvent);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> SchoolExistsAsync(Guid schoolId)
        {
            return await _db.Events.AnyAsync(s => s.SchoolId == schoolId);
        }

        public async Task<SchoolEvent?> GetEventByTitleAsync(string title, Guid schoolId)
        {
            return await _db.Events
                .FirstOrDefaultAsync(e => e.Title == title && e.SchoolId == schoolId);
        }


    }


}
