using Microsoft.EntityFrameworkCore;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Entities.JobEntities;
using SchoolMagazine.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Infrastructure.Data.RepositoryImplementation
{
    // In Infrastructure/Data/RepositoryImplementation
    public class SchoolDashboardRepository : ISchoolDashboardRepository
    {
        private readonly MagazineContext _db;

        public SchoolDashboardRepository(MagazineContext db)
        {
            _db = db;
        }

        public async Task<List<SchoolEvent>> GetEventsAsync(Guid schoolId, DateTime start, DateTime end)
        {
            return await _db.Events
                .Where(e => e.SchoolId == schoolId && e.EventDate >= start && e.EventDate <= end)
                .Include(e => e.EventMediaItems) // optional: if you need media items
                .ToListAsync();
        }


        public async Task<List<SchoolAdvert>> GetAdvertsAsync(Guid schoolId, DateTime start, DateTime end)
        {
            return await _db.Adverts
                .Where(a => a.SchoolId == schoolId && a.StartDate >= start && a.EndDate <= end)
                .ToListAsync();
        }

        public async Task<List<JobPost>> GetJobPostsWithApplicationsAsync(Guid schoolId, DateTime start, DateTime end)
        {
            return await _db.JobPosts
                .Where(j => j.SchoolId == schoolId && j.PostedAt >= start && j.PostedAt <= end)
                .Include(j => j.Applications)
                .ThenInclude(a => a.Users)
                .ToListAsync();
        }
    }

}
