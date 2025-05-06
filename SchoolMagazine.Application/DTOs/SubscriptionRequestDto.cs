using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class SubscriptionRequestDto
    {
        public Guid VendorId { get; set; }

        [Range(5000, double.MaxValue, ErrorMessage = "Minimum subscription amount is ₦5,000.")]
        public decimal AmountPaid { get; set; }
    }
}
