using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.UserRoleInfo;

namespace SchoolMagazine.Domain.Entities.JobEntities
{
    public class JobApplication
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid JobPostingId { get; set; }
        public JobPost JobPosting { get; set; }

        public Guid UserId { get; set; } // FK to Applicant
        public User Users { get; set; }

        public string CVUrl { get; set; } // uploaded CV path
        public string CoverLetter { get; set; }
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}
