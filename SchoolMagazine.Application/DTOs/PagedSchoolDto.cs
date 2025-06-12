using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class PagedSchoolDto
    {
       
        public string SchoolName { get; set; }

        public string WebsiteUrl { get; set; }
      

        //[JsonIgnore]
        public ICollection<SchoolEventDto>? Events { get; set; }
        //[JsonIgnore]

        // public ICollection<SchoolAdvertDto>? Adverts { get; set; }
        public ICollection<SchoolAdvertDto>? Adverts { get; set; } = new List<SchoolAdvertDto>();

    }

}

