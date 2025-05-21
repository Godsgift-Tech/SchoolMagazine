using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class JobPostNotificationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Qualification { get; set; }
        public string Location { get; set; }
        public DateTime PostedAt { get; set; }
    }
}
