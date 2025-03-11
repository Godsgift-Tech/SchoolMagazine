using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class PaymentRequestDto
    {
       // public Guid SchoolId { get; set; }
        public Guid AdvertId { get; set; }  // Ensure this exists
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string PaymentMethod { get; set; }  // e.g., "Credit Card", "Paystack", "Flutterwave"
    }


    //public class PaymentRequestDto
    //{
    //    public Guid AdvertId { get; set; }  // Ensure this exists
    //    public decimal Amount { get; set; }
    //    public string Currency { get; set; } = "USD";
    //    public string PaymentMethod { get; set; }
    //}

}
