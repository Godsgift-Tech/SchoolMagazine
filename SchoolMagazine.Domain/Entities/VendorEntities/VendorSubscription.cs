using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.VendorEntities
{
    public class VendorSubscription
    {
        
            [Key]
            public Guid Id { get; set; }

            [Required]
            public Guid VendorId { get; set; }

            [Required]
            public DateTime StartDate { get; set; } = DateTime.UtcNow;

            [Required]
            public DateTime ExpiryDate { get; set; }

            [Required]
            public decimal AmountPaid { get; set; } = 5000; // Default ₦5000

            public Vendor Vendor { get; set; }
        }

    }

