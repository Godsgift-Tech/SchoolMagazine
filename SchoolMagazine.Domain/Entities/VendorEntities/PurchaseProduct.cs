using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Domain.Entities.VendorEntities
{
    public class PurchaseProduct
    {
        public Guid SchoolPurchaseId { get; set; }
        public Purchases Schoolpurchase { get; set; }
        public Guid ProductId { get; set; }
        public SchoolProduct Product { get; set; }
        public int Quantity { get; set; }
    }

  
}
