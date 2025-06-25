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
        [JsonIgnore]   // the Id is auto-generated

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();


        [Required]
        public string SchoolName { get; set; }

        [JsonIgnore]   // the Id is auto-generated

        public string? UserId { get; set; } = Guid.NewGuid().ToString();

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
        [Required]
        public string EmailAddress { get; set; } // email address [Required]

        public List<string> ImageUrls { get; set; } = new();

        [Required]
        public string PhoneNumber { get; set; } // phone number
        [Required]
        public string WebsiteUrl { get; set; }
        [Required]
        public decimal FeesRange { get; set; }
        //[Required]
        //public double Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [JsonIgnore]
        public ICollection<SchoolEventDto>? Events { get; set; }
        [JsonIgnore]

        public ICollection<SchoolAdvertDto>? Adverts { get; set; } = new List<SchoolAdvertDto>();
    }
}
