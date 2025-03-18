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
        public Guid Id { get; set; } 


        [Required(ErrorMessage = "Title is required.")]
        [StringLength(40, ErrorMessage = "Title cannot exceed 40 characters.")]
        public string SchoolName { get; set; }
        
        public Guid UserId { get; set; } 

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
      // [JsonIgnore]

       public ICollection<SchoolAdvert>? Adverts { get; set; } = new List<SchoolAdvert>();
       // public ICollection<Advert> Adverts { get; set; } = new List<Advert>();
    }
}