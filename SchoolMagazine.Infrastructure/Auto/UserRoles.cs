using Microsoft.AspNet.Identity.EntityFramework;
using SchoolMagazine.Application.AppUsers;
using SchoolMagazine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Infrastructure.Auto
{
    public class UserRoles: IdentityUserRole<Guid>
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
