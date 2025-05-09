﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class CreateAdvertDto
    {
        [JsonIgnore]   // the Id is auto-generated  

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]

        public string Content { get; set; }


        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]

        public Guid SchoolId { get; set; }
       
        [Required]
        [Range(1000, double.MaxValue, ErrorMessage = "Amount paid must be <= 1000.")]
        public decimal AmountPaid { get; set; }

        // Optional: List of files (images or videos) for the advert
        //public List<string>? MediaUrls { get; set; } = new List<string>();
        public List<MediaItemDto>? MediaItems { get; set; }


    }
}
