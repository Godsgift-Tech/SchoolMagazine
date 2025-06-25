using SchoolMagazine.Domain.UserRoleInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class SchoolRating
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid SchoolId { get; set; }

        [ForeignKey("SchoolId")]
        public School School { get; set; }

        // Either one of these can be filled, not both
        public Guid? UserId { get; set; }  // Ordinary user
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public Guid? SchoolAdminId { get; set; }  // Admin who posted the school
        [ForeignKey("SchoolAdminId")]
        public User? SchoolAdmin { get; set; }

        [Range(1, 10)]
        public int RatingValue { get; set; }

        public string? Comment { get; set; }

        public DateTime RatedAt { get; set; } = DateTime.UtcNow;
    }
}
