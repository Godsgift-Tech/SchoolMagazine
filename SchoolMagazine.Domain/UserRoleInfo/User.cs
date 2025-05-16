using Microsoft.AspNetCore.Identity;
using SchoolMagazine.Domain.Entities.JobEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.UserRoleInfo
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<JobPost> PostedJobs { get; set; } = new List<JobPost>();
        public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
        public ICollection<JobMessage> SentMessages { get; set; } = new List<JobMessage>();
        public ICollection<JobMessage> ReceivedMessages { get; set; } = new List<JobMessage>();
        public ICollection<JobNotificationSubscription> JobAlertSubscriptions { get; set; } = new List<JobNotificationSubscription>();
    }
}

