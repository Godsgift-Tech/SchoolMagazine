using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class JobPostNotificationDto
    {
        public string Title { get; set; } = string.Empty;

       // public string Description { get; set; } = string.Empty;
        public string? Description { get; set; }


        public string Qualification { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        // List of category names for matching subscriber preferences
        public List<string> Categories { get; set; } = new();

       // public DateTime PostedAt { get; set; }
        public DateTime PostedAt { get; set; }


        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public string JobType { get; set; } = string.Empty;

        public string ExperienceLevel { get; set; } = string.Empty;


    }
}
