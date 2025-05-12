using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.DTOs
{
    public class PurchaseProductResponseDto
    {
        public string ProductName { get; set; }
        public int QuantityRequested { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }

        public string VendorEmail { get; set; }
        public string VendorPhone { get; set; }
        public string VendorBankName { get; set; }
        public string VendorBankAccountNumber { get; set; }
        public string VendorAccountName { get; set; }

        public int AvailableQuantity { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
