using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.VendorEntities
{
    public class Product
    {
       
            [Key]
            public Guid Id { get; set; }

            [Required]
            public Guid VendorId { get; set; }  // Foreign Key

            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [MaxLength(500)]
            public string Description { get; set; }

            [Column(TypeName = "decimal(18,2)")]
            public decimal Price { get; set; }

            [Required]
            public int AvailableQuantity { get; set; }

            [MaxLength(100)]
            public string Category { get; set; }

            // Navigation Properties
            [ForeignKey(nameof(VendorId))]
            public Vendor Vendor { get; set; }

           // public ICollection<SchoolPurchaseProduct> SchoolPurchaseProducts { get; set; } = new List<SchoolPurchaseProduct>();
        }
    }


