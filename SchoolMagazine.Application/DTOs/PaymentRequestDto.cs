using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class PaymentRequestDto
    {
        public decimal AmountPaid { get; set; }
        public string PaymentReference { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    }


   

}
