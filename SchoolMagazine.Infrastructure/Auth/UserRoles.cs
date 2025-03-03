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
        
    }
}
