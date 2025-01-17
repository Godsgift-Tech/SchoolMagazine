using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Invalid Password lenght", MinimumLength = 6)]
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin, School, Visitor
    }
}
