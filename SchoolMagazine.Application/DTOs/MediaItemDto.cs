using SchoolMagazine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class MediaItemDto
    {
        public string Url { get; set; }
        public MediaType MediaType { get; set; }  // Enum (Image or Video)

    }
}
