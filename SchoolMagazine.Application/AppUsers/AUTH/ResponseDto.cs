using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppUsers.AUTH
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

    }
}
