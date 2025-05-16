using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.UserRoleInfo;

namespace SchoolMagazine.Domain.Entities.JobEntities
{
    public class JobMessage
    {
        
            public Guid Id { get; set; } = Guid.NewGuid();
            public Guid JobId { get; set; }
            public JobPost JobPost { get; set; }

            public Guid SenderId { get; set; }
            public User Sender { get; set; }

            public Guid ReceiverId { get; set; }
            public User Receiver { get; set; }

            public string Subject { get; set; }
            public string Body { get; set; }

            public DateTime SentAt { get; set; } = DateTime.UtcNow;
            public bool IsRead { get; set; } = false;
        

    }
}
