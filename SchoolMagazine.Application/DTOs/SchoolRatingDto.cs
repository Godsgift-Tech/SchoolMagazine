using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class SchoolRatingDto
    {
        public Guid Id { get; set; }

        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; }

        public Guid? UserId { get; set; }
        public string? UserName { get; set; }

        public Guid? SchoolAdminId { get; set; }
        public string? SchoolAdminName { get; set; }
        [Range(1, 10)]

        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public DateTime RatedAt { get; set; }
    }
}
