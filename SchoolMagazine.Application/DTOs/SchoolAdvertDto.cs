﻿using SchoolMagazine.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolMagazine.Application.DTOs
{
    public class SchoolAdvertDto
    {
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
        public decimal AmountPaid { get; set; } //  Amount Paid for the Advert
        public bool IsPaid { get; set; }  // dont think it is neccessary
        public string? PaymentReference { get; set; }  // Ensure this exists and is nullable
        public DateTime? PaymentDate { get; set; }

        public List<MediaItemDto> MediaItems { get; set; } = new();

    }





}
