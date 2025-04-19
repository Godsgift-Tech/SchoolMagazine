using SchoolMagazine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class SchoolEventMedia
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Url { get; set; }  // Image or video URL

        public SchoolEvent SchoolEvent { get; set; } // Navigation property


        public MediaType MediaType { get; set; }  // Enum (Image or Video)

        [Required]
        public Guid SchoolEventId { get; set; }

        //public SchoolEvent? SchoolEvent { get; set; }
    }
}
