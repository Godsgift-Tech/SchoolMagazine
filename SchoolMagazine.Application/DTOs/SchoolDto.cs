using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class SchoolDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string AdminName { get; set; }

        public Guid UserId { get; set; } // Foreign key to User (admin)

        // Navigation Properties
        [Required]
        public User User { get; set; }

        [Required]
        public string Location { get; set; } //address

      
        [Required]
        public string PhoneNumber { get; set; } // phone number
        [Required]
        public string WebsiteUrl { get; set; }
        [Required]
        public decimal FeesRange { get; set; }
        [Required]
        public double Rating { get; set; }
        public ICollection<SchoolEventDto> Events { get; set; }
        public ICollection<SchoolAdvertDto> Adverts { get; set; }
    }





}
