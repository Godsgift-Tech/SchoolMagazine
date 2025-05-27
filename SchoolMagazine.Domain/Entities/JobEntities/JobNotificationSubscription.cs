using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.UserRoleInfo;

namespace SchoolMagazine.Domain.Entities.JobEntities
{
    public class JobNotificationSubscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public User Users { get; set; }
       
        // Preferences
        public string JobType { get; set; } // Remote, Onsite, Hybrid

        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }

        public string ExperienceLevel { get; set; } // Entry, Mid, Senior

        public string NotificationFrequency { get; set; } // Daily, Weekly, Immediate

      //  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation: Preferred job categories
        public ICollection<JobCategoryPreference> Categories { get; set; } = new List<JobCategoryPreference>();
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;  
    }
}
