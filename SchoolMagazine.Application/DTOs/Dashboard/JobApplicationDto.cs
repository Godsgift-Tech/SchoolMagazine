using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs.Dashboard
{
    public class JobApplicationDto
    {
        public Guid Id { get; set; }

        public Guid JobPostingId { get; set; }
        public string JobTitle { get; set; } // Optional: you can map this from JobPosting.Title

        public Guid UserId { get; set; }
        public string ApplicantFullName { get; set; } // from User.FirstName + LastName
        public string ApplicantEmail { get; set; }    // from User.Email

        public string CVUrl { get; set; } // uploaded CV path
        public string CoverLetter { get; set; }

        public DateTime AppliedAt { get; set; }
    }
}
