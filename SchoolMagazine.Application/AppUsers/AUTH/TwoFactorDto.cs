using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppUsers.AUTH
{
    public class TwoFactorDto
    {
        //public string Email { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
        public string Provider { get; set; } = TokenOptions.DefaultAuthenticatorProvider;  // Default 2FA provider
    }
}
