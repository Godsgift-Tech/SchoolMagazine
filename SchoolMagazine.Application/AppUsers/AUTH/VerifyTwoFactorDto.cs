﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppUsers.AUTH
{
    public class VerifyTwoFactorDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
