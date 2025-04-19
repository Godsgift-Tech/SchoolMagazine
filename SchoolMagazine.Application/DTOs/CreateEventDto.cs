using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class CreateEventDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        public Guid SchoolId { get; set; }


        public DateTime EventDate { get; set; }

        public List<MediaItemDto>? MediaItems { get; set; }
    }
}
