using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.VendorEntities
{
    public class SchoolVendor
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(150)]
        public string CompanyName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string CAC_Number { get; set; }

        [Required]
        [MaxLength(50)]
        public string TIN { get; set; }
        public bool IsApproved { get; set; }
        public bool HasActiveSubscription { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        // Vendor contact and bank info to display on product purchase
        [MaxLength(200)]
        public string BankName { get; set; }

        [MaxLength(10)]
        public string AccountNumber { get; set; }

        [MaxLength(50)]
        public string AccountName { get; set; }

      
        public ICollection<SchoolProduct> Products { get; set; }
    }
}
