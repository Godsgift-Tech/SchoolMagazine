using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.JobEntities
{
    public class JobCategoryPreference
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string CategoryName { get; set; }

        public Guid SubscriptionId { get; set; }
        public JobNotificationSubscription Subscription { get; set; }
    }
}
