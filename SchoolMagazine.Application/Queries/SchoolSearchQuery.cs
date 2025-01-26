using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.Queries
{
    public class SchoolSearchQuery
    {
        public string Location { get; set; }
        public decimal? FeesRange { get; set; }
        public double? Rating { get; set; }
    }
}
