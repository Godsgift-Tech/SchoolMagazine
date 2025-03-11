using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class AdvertPaymentRequestDto
    {
        public SchoolAdvertDto Advert { get; set; }
        public PaymentRequestDto PaymentDetails { get; set; }
    }
}
