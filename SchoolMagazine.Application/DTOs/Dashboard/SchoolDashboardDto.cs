using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs.Dashboard
{
    public class SchoolDashboardDto
    {
        public List<SchoolEventDto> Events { get; set; }
        public List<SchoolAdvertDto> Adverts { get; set; }
        public List<JobPostDto> JobPosts { get; set; }
        public List<JobApplicationDto> Applications { get; set; }
    }
}
