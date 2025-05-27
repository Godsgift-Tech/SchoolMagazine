using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class JobNotificationSubscriptionDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        
        public string JobType { get; set; } = "Remote"; // Remote, Onsite, Hybrid
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string ExperienceLevel { get; set; } = "Entry"; // Entry, Mid, Senior
        public string NotificationFrequency { get; set; } = "Immediate"; // Daily, Weekly, Immediate
        public List<string> Categories { get; set; } = new();
        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    }
}
