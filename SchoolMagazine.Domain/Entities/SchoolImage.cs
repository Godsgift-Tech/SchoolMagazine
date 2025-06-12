using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class SchoolImage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } // You can store URL or file path

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Foreign key to School
        public Guid SchoolId { get; set; }

        // Navigation property
        public School School { get; set; }
    }
}
