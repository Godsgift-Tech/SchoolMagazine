using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService.Paged
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; }
        public List<T> Items { get; set; }
    }
}
