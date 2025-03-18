using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class CreateSchoolDto
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();


        [Required]
        public string SchoolName { get; set; }


        public string UserId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Location { get; set; } //address
        [Required]
        public string EmailAddress { get; set; } // email address [Required]


        [Required]
        public string PhoneNumber { get; set; } // phone number
        [Required]
        public string WebsiteUrl { get; set; }
        [Required]
        public decimal FeesRange { get; set; }
        [Required]
        public double Rating { get; set; }

        [JsonIgnore]
        public ICollection<SchoolEventDto>? Events { get; set; }
        [JsonIgnore]

        // public ICollection<SchoolAdvertDto>? Adverts { get; set; }
        public ICollection<SchoolAdvertDto>? Adverts { get; set; } = new List<SchoolAdvertDto>();
    }
}
