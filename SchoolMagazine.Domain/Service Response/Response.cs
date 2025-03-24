using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Service_Response
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
