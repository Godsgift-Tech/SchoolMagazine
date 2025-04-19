using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
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


        public async Task<PagedResult<SchoolEvent>> GetAllEventsAsync(int pageNumber, int pageSize)
        {
            var query = _db.Events
                .Include(e => e.School)
                .Include(e => e.EventMediaItems)
                .Where(e => e.EventDate >= DateTime.UtcNow) // Filter: only upcoming events
                .OrderByDescending(e => e.EventDate)        // Latest first
                .AsQueryable();

            int totalCount = await query.CountAsync();

            var events = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<SchoolEvent>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = events
            };
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
                .Include(e => e.School)
                .Include(e => e.EventMediaItems)
                .FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task<IEnumerable<SchoolEvent>> GetEventsBySchoolAsync(string schoolName)
        {
            return await _db.Events
                .Include(e => e.School)  // Include School entity
                .Where(e => e.School.SchoolName == schoolName) // Filter by SchoolName
                .ToListAsync();
        }




        public async Task<IEnumerable<SchoolEvent>> GetEventsByName(string eventName)
        {
            return await _db.Events
                .Include(e => e.School) // Navigation property
                .Include(e => e.EventMediaItems) // Media items if needed
                .Where(e => e.Title == eventName)
                .AsNoTracking()
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
                // Optionally prevent duplicate events (by title and description)
                var existingEvent = await _db.Events
                    .FirstOrDefaultAsync(e =>
                        e.Title.ToLower() == eventDetails.Title.ToLower().Trim() &&
                        e.Description.ToLower() == eventDetails.Description.ToLower().Trim() &&
                        e.SchoolId == eventDetails.SchoolId);

                if (existingEvent != null)
                {
                    response.Success = false;
                    response.Message = "An event with the same title and description already exists for this school.";
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
                existingEvent.EventMediaItems = eventDetails.EventMediaItems;
               
                // Update other properties as needed

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

        public async Task<PagedResult<SchoolEvent>> GetEventsAsync(
    string? title, string? description, Guid? schoolId, string? schoolName, int pageNumber, int pageSize)
        {
            var query = _db.Events.Include(e => e.School).AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(e => e.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(description))
                query = query.Where(e => e.Description.Contains(description));

            if (schoolId.HasValue)
                query = query.Where(e => e.SchoolId == schoolId.Value);

            if (!string.IsNullOrWhiteSpace(schoolName))
                query = query.Where(e => e.School.SchoolName.Contains(schoolName));

            int totalCount = await query.CountAsync();

            var events = await query
                 .Include(e => e.EventMediaItems)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<SchoolEvent>
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = events
            };
        }




    }


}
