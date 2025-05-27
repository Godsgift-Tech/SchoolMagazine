using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.UserRoleInfo;

namespace SchoolMagazine.Domain.Entities.JobEntities
{
    public class JobPost
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Qualification { get; set; }

        public string Location { get; set; }
        // newly added
        [Required]
        public string JobType { get; set; } // Remote, Onsite, Hybrid

        [Range(0, double.MaxValue)]
        public decimal MinSalary { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MaxSalary { get; set; }

        [Required]
        public string ExperienceLevel { get; set; } // Entry, Mid, Senior

        [Required]
        public string Category { get; set; } // E.g., Teaching, Admin, Finance

        //  old properties
        public bool IsAvailable { get; set; } = true;

        public DateTime PostedAt { get; set; } = DateTime.UtcNow;

        // School Admin who posted
        public Guid PostedById { get; set; }
        public User PostedBy { get; set; }

        // School the job is for
        public Guid SchoolId { get; set; }
        public School School { get; set; }

        // Navigation: applicants
        public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();

        // Navigation: messages to applicants
        public ICollection<JobMessage> Messages { get; set; } = new List<JobMessage>();
    }

}
