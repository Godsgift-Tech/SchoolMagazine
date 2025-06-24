using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs.Dashboard
{
    public class JobPostDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Qualification { get; set; }

        public string Location { get; set; }

        public string JobType { get; set; } // Remote, Onsite, Hybrid

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public string ExperienceLevel { get; set; } // Entry, Mid, Senior

        public string Category { get; set; } // Teaching, Admin, etc.

        public bool IsAvailable { get; set; }

        public DateTime PostedAt { get; set; }

        public Guid PostedById { get; set; }

        public string PostedByFullName { get; set; } // from PostedBy.FirstName + LastName

        public Guid SchoolId { get; set; }

        public string SchoolName { get; set; } // from School.SchoolName

        public int NumberOfApplications { get; set; } // from Applications.Count
    }
}
