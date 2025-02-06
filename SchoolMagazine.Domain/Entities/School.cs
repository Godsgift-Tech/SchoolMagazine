using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class School
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 

        [Required]
        public string SchoolName { get; set; }
        
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        // Navigation Properties
        
      //  public User User { get; set; }
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
         public ICollection<SchoolEvent>? Events { get; set; }
       [JsonIgnore]

       public ICollection<SchoolAdvert>? Adverts { get; set; }
    }
}