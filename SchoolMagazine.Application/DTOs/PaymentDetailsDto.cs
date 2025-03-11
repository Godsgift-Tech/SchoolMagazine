using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class PaymentDetailsDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string PaymentMethod { get; set; } = "Card";
        public Guid AdvertId { get; set; } // Ensure the payment is linked to an advert
    }

    //
   
    

}
