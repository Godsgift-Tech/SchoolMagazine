using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppUsers.AUTH
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string[] Errors { get; set; }
    }
}
