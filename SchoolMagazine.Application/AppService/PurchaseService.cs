using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppService
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IProductRepository _pR;
        private readonly IMapper _mapper;


        public PurchaseService(IProductRepository pR, IMapper mapper)
        {
            _pR = pR;
            _mapper = mapper;
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


       

        public async Task<ServiceResponse<PagedResult<SchoolProductDto>>> GetAllProductAsync(string? name, string? category, Guid? vendorId, int pageNumber, int pageSize)
        {
            var pagedProducts = await _pR.GetPagedProductsAsync(name, category, vendorId, pageNumber, pageSize);

            if (pagedProducts.Items.Count == 0)
                return new ServiceResponse<PagedResult<SchoolProductDto>>(null!, false, "No product found matching criteria.");

            var productDtos = _mapper.Map<List<SchoolProductDto>>(pagedProducts.Items);

            var result = new PagedResult<SchoolProductDto>
            {
                TotalCount = pagedProducts.TotalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = productDtos
            };

            return new ServiceResponse<PagedResult<SchoolProductDto>>(result, true, "School Products retrieved successfully.");
        }


    }
}
