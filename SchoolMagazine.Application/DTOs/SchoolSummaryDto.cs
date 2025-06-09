using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class SchoolSummaryDto
    {
        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public string WebsiteUrl { get; set; }
    }
}
