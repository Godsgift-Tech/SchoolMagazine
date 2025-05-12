using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class PurchaseProductRequestDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
