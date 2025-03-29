using SchoolMagazine.Domain.UserRoleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppInterface
{
    public interface ITokenService
    {
        // string GenerateToken(User user, List<string> roles);
        // string GenerateToken(ApplicationUser user, IList<string> roles);
        //string GenerateToken(User user, List<string> roles);
        string CreateJWTToken(User user, List<string> roles);
        //string GenerateToken(User user, List<string> roles);
        //object GenerateToken(User user, IList<string> userRoles);
    }
}
