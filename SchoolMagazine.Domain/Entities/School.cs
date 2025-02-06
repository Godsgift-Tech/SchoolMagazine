using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class School
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string AdminName { get; set; }

        public Guid UserId { get; set; } // Foreign key to User (admin)

        // Navigation Properties
        
      //  public User User { get; set; }
        [Required]
        public string Location { get; set; } //address

        [Required]
        public string EmailAddress { get; set; } // email address [Required]
        [Required]
        
        public string WebsiteUrl { get; set; }
        [Required]
        public decimal FeesRange { get; set; }
        [Required]
        public double Rating { get; set; }
        public ICollection<SchoolEvent> Events { get; set; }
        public ICollection<SchoolAdvert> Adverts { get; set; }
    }
}