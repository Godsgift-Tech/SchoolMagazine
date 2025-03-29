using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class PaymentResponseDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? TransactionId { get; set; } // Unique transaction ID if payment is successful
    }
}
