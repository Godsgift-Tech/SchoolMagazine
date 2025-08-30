using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class SearchedSchoolDto
    {
       
        //[Required]
        //public string SchoolName { get; set; }
                
        [Required]
        public string Location { get; set; } //address
        [Required]

        [MaxLength(100)]
        public string City { get; set; }
        [Required]

        [MaxLength(100)]
        public string State { get; set; }
        [Required]

        [MaxLength(100)]
        public string Country { get; set; }
        public List<string> ImageUrls { get; set; } = new();
        [Required]
        public string WebsiteUrl { get; set; }
      

    }
}
