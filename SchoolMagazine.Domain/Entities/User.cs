﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
       // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       // public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
       // public bool IsActive { get; set; } = true;

       
    }
}
