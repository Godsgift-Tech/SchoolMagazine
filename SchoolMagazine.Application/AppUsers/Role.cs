using Microsoft.AspNetCore.Identity;

namespace SchoolMagazine.Application.AppUsers
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }   // Admin, School, Visitor
    }



}
