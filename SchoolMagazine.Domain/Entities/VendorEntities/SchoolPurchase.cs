using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.VendorEntities
{
   public class SchoolPurchase
    {
        public Guid Id { get; set; }
        public Guid SchoolAdminId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
       // public ICollection<SchoolPurchaseProduct> SchoolPurchaseProducts { get; set; }
        public ICollection<SchoolPurchaseProduct> SchoolPurchaseProducts { get; set; } = new List<SchoolPurchaseProduct>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
    
}

