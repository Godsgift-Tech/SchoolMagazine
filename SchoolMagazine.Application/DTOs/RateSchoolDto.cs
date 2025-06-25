using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class RateSchoolDto
    {
        public Guid? UserId { get; set; }  // Required if not SchoolAdminId
        public Guid? SchoolAdminId { get; set; }  // Optional
        public string SchoolName { get; set; }
        [Range(1, 10)]
        public int RatingValue { get; set; }  // 1-10
        public string? Comment { get; set; }
    }
}
