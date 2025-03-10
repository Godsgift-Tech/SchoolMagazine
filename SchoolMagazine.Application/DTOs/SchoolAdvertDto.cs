using SchoolMagazine.Domain.Entities;
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
        public School SchoolName { get; set; }
    }





}
