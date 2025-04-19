using SchoolMagazine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class SchoolAdvertMedia
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SchoolAdvertId { get; set; }

        public SchoolAdvert SchoolAdvert { get; set; } // Navigation property

        [Required]
        public string Url { get; set; } // URL or file path to the media

        [Required]
        public MediaType MediaType { get; set; } // Enum to specify if it's an image or video
    }
   

}
