using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class PaymentResponseDto
    {
        public bool Success { get; set; }  // Indicates if the payment was successful
        public string Message { get; set; }  // Message describing the payment result
        public string? TransactionId { get; set; }  // Unique identifier for the transaction (optional)
    }
}
