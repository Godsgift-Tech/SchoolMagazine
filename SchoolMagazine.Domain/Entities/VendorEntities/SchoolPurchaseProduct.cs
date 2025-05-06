using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.VendorEntities
{
    public class SchoolPurchaseProduct
    {
        public Guid SchoolPurchaseId { get; set; }
        public SchoolPurchase SchoolPurchase { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

  
}
