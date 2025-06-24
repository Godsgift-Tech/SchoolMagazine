using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Application.DTOs.Dashboard;
using SchoolMagazine.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService
{
    public class SchoolDashboardService : ISchoolDashboardService
    {
        private readonly ISchoolDashboardRepository _repo;
        private readonly IMapper _mapper;

        public SchoolDashboardService(ISchoolDashboardRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SchoolDashboardDto> GetDashboardDataAsync(Guid schoolId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var start = fromDate ?? DateTime.MinValue;
            var end = toDate ?? DateTime.MaxValue;

            var events = await _repo.GetEventsAsync(schoolId, start, end);
            var adverts = await _repo.GetAdvertsAsync(schoolId, start, end);
            var jobPosts = await _repo.GetJobPostsWithApplicationsAsync(schoolId, start, end);
            var applications = jobPosts.SelectMany(j => j.Applications).ToList();

            return new SchoolDashboardDto
            {
                Events = _mapper.Map<List<SchoolEventDto>>(events),
                Adverts = _mapper.Map<List<SchoolAdvertDto>>(adverts),
                JobPosts = _mapper.Map<List<JobPostDto>>(jobPosts),
                Applications = _mapper.Map<List<JobApplicationDto>>(applications)
            };
        }
    }

}
