using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.VendorEntities
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string TIN { get; set; }
        public bool IsApproved { get; set; }
        public bool HasActiveSubscription { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
