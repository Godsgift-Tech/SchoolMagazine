using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Entities.JobEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.RepositoryInterface
{
    public interface ISchoolDashboardRepository
    {
        Task<List<SchoolEvent>> GetEventsAsync(Guid schoolId, DateTime start, DateTime end);
        Task<List<SchoolAdvert>> GetAdvertsAsync(Guid schoolId, DateTime start, DateTime end);
        Task<List<JobPost>> GetJobPostsWithApplicationsAsync(Guid schoolId, DateTime start, DateTime end);
    }
}
