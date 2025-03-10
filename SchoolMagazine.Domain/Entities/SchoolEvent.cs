using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class SchoolEvent
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        public Guid SchoolId { get; set; }     // School Id
        public School School { get; set; }   // Navigation Property(virtual makes Ef recognize as Navigation)
        [Required]

        public DateTime EventDate { get; set; }

        public string? MediaUrl { get; set; }  // Images/Videos 
    }
}
