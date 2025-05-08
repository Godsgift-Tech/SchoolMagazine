using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.VendorEntities
{
    [Table("NewSchoolPurchases")] 
   public class Purchases
    {
        public Guid Id { get; set; }
        public Guid SchoolAdminId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
       // public ICollection<SchoolPurchaseProduct> SchoolPurchaseProducts { get; set; }
        public ICollection<PurchaseProduct> SchoolPurchaseProducts { get; set; } = new List<PurchaseProduct>();
        public ICollection<SchoolProduct> Products { get; set; } = new List<SchoolProduct>();
    }
    
}

