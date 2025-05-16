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
        public DateTime SubscribedAt { get; set; }  
    }
}
