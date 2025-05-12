using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Interface;

namespace SchoolMagazine.Application.AppService
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IProductRepository _pR;

        public PurchaseService(IProductRepository pR)
        {
            _pR = pR;
        }

        public async Task<PurchaseProductResponseDto> CalculatePurchaseAsync(PurchaseProductRequestDto request)
        {
            var product = await _pR.GetProductWithVendorAsync(request.ProductId);

            if (product == null)
            {
                return new PurchaseProductResponseDto
                {
                    Success = false,
                    Message = "Product not found"
                };
            }

            if (request.Quantity <= 0 || request.Quantity > product.AvailableQuantity)
            {
                return new PurchaseProductResponseDto
                {
                    Success = false,
                    Message = "Insufficient quantity!" + product.AvailableQuantity + " only is  what is available"
                };
            }


            var total = request.Quantity * product.Price;

            return new PurchaseProductResponseDto
            {
                ProductName = product.Name,
                QuantityRequested = request.Quantity,
                UnitPrice =product.Price,
                AvailableQuantity =product.AvailableQuantity,
                TotalAmount = total,
                VendorBankName = product.Vendor.BankName,
                VendorEmail = product.Vendor.Email,
                VendorBankAccountNumber = product.Vendor.AccountNumber,
                VendorAccountName = product.Vendor.AccountName,
                VendorPhone = product.Vendor.PhoneNumber, // adjust as needed
                Success = true,
                Message = "Calculation successful."
            };
        }



    }
}
