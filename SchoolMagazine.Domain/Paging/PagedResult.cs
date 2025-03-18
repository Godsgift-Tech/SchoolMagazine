using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Paging
{
    public class PagedResult <T>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; } 
        public int PageNumber { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
