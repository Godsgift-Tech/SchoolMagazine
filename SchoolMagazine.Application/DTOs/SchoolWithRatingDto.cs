using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class SchoolWithRatingDto
    {
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; }
        public double AverageRating { get; set; }
    }
}
