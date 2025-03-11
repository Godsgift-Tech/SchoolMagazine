using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class SchoolAdvert
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]

        public string Content { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]

        public Guid SchoolId { get; set; }
        public  School School { get; set; }   // Navigation Property(virtual makes Ef recognize as Navigation)

        public bool IsPaid { get; set; }=false;
        public string? PaymentReference { get; set; }  // Ensure this exists and is nullable
        public DateTime? PaymentDate { get; set; }
    }
}
