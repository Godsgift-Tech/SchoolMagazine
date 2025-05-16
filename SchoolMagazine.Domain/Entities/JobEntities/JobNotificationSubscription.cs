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

        public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
    }
}
