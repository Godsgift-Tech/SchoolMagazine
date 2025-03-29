using SchoolMagazine.Domain.UserRoleInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Service_Response
{
    public class UserResponse
    {
        public string Token { get; set; } = null;
        public User User { get; set; } = null;
    }
}
