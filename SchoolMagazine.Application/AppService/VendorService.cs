﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;

namespace SchoolMagazine.Application.AppService
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vR;
        private readonly IProductRepository _pR;
        private readonly IMapper _mapper;


        public VendorService(IVendorRepository vendorRepository, IProductRepository productRepository, IMapper mapper)
        {
            _vR = vendorRepository;
            _pR = productRepository;
            _mapper = mapper;
        }
     
        public async Task<ServiceResponse<VendorDto>> AddVendorAsync(VendorDto vendorDto)
        {
            // Map DTO to Entity
            var vendor = _mapper.Map<SchoolVendor>(vendorDto);

            // Call repository to add vendor
            var result = await _vR.AddVendorAsync(vendor);

            if (!result.success)
                return new ServiceResponse<VendorDto>(null!, false, result.message);

            // Map back to DTO (optional since you may not use it here)
            var newVendorDto = _mapper.Map<VendorDto>(vendor);

            return new ServiceResponse<VendorDto>(newVendorDto, true, result.message);
        }

        public async Task<ServiceResponse<SchoolVendor>> ApproveVendorAsync(Guid vendorId)
        {
        var vendor = await _vR.GetVendorByIdAsync(vendorId);
            if (vendor == null)
            
                return new ServiceResponse<SchoolVendor>(null!, false, "Vendor not found");
            vendor.IsApproved = true;
            await _vR.UpdateVendorAsync(vendor);
                return new ServiceResponse<SchoolVendor>(null!, true, "Vendor approved successfully!");

        }

        public async Task CreateProductAsync(Guid vendorId, SchoolProduct product)
        {
            var vendor = await _vR.GetVendorByIdAsync(vendorId);
            if (vendor == null) throw new Exception("Vendor not found");
            if (!vendor.IsApproved) throw new Exception("Only approved vendors can create products!");
            if (!await _vR.HasActiveSubscriptionAsync(vendorId)) throw new 
                    Exception("Vendor does not have active subscription");
            product.VendorId = vendorId;
           await _pR.AddAsync(product);
        }

        public async Task<ServiceResponse<CreateProductDto>> CreateProductAsync(Guid vendorId, CreateProductDto productDto)
        {
            var vendor = await _vR.GetVendorByIdAsync(vendorId);
            if (vendor == null)
                return new ServiceResponse<CreateProductDto>(null!, false, "Vendor not found.");

            if (!vendor.IsApproved)
                return new ServiceResponse<CreateProductDto>(null!, false, "Only approved vendors can create products.");

            if (!await _vR.HasActiveSubscriptionAsync(vendorId))
                return new ServiceResponse<CreateProductDto>(null!, false, "Vendor does not have an active subscription.");

            var product = _mapper.Map<SchoolProduct>(productDto);
            product.VendorId = vendorId;

            await _pR.AddAsync(product);

            var responseDto = _mapper.Map<CreateProductDto>(product);
            return new ServiceResponse<CreateProductDto>(responseDto, true, "Product created successfully.");
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var findProduct = await _pR.GetByProductIdAsync(productId);
            if (findProduct == null) throw new Exception("Product not found");
            await _pR.DeleteProductAsync(findProduct);
        }

        public async Task DeleteVendorAsync(Guid vendorId)
        {
         var vendor = await _vR.GetVendorByIdAsync(vendorId);
            if (vendor == null) throw new Exception("Vendor not found");
            await _vR.DeleteVendorAsync(vendor);
        }

   
        public async Task<PagedResult<ApprovedVendorDto>> GetAllApprovedVendorsAsync(int pageNumber, int pageSize)
        {
            var result = await _vR.GetAllApprovedVendorsAsync(pageNumber, pageSize);

            var vendorDtos = _mapper.Map<List<ApprovedVendorDto>>(result.Items);

            return new PagedResult<ApprovedVendorDto>
            {
                TotalCount = result.TotalCount,
                PageSize = result.PageSize,
                PageNumber = result.PageNumber,
                Items = vendorDtos
            };
        }

        public async Task<SchoolVendor> GetVendorByIdAsync(Guid vendorId)
        {
            var findVendor = await _vR.GetVendorByIdAsync(vendorId);
            if (findVendor == null) throw new Exception("Vendor does not exist");
            return findVendor;  
        }

        public async Task<bool> HasActiveSubscriptionAsync(Guid vendorId)
        {
            var hasSubscription = await _vR.HasActiveSubscriptionAsync(vendorId);

            if (!hasSubscription)
                throw new Exception("This vendor does not have an active subscription.");

            return true;
        }


        public async Task SubscribeVendorAsync(Guid vendorId) => await _vR.SubscribeVendorAsync(vendorId);

        public async Task<ServiceResponse<string>> SubscribeVendorAsync(SubscriptionRequestDto request)
        {
            var vendor = await _vR.GetVendorByIdAsync(request.VendorId);

            if (vendor == null)
                return new ServiceResponse<string>(null!, false, "Vendor not found.");

            if (request.AmountPaid < 5000)
                return new ServiceResponse<string>(null!, false, "Minimum payment is ₦5,000.");

            // Calculate months
            var months = (int)(request.AmountPaid / 5000);

            vendor.HasActiveSubscription = true;
            vendor.AmountPaid += request.AmountPaid;
            vendor.SubscriptionStartDate = DateTime.UtcNow;

            vendor.SubscriptionEndDate = vendor.SubscriptionEndDate != null && vendor.SubscriptionEndDate > DateTime.UtcNow
                ? vendor.SubscriptionEndDate.Value.AddMonths(months)
                : DateTime.UtcNow.AddMonths(months);

            await _vR.UpdateVendorAsync(vendor);

            return new ServiceResponse<string>($"Subscription successful for {months} month(s).", true);
        }


        public async Task<ServiceResponse<SchoolProduct>> UpdateProductAsync(Guid vendorId, Guid productId, UpdateProductDto productDto)
        {
            // Check if vendor exists
            var vendor = await _vR.GetVendorByIdAsync(vendorId);
            if (vendor == null)
            return new ServiceResponse<SchoolProduct>(null!, success: false, message: "Vendor not found");

            //
            if (!vendor.IsApproved)
                return new ServiceResponse<SchoolProduct>(null!, success: false, message: "Vendor is not approved");

            if (!await _vR.HasActiveSubscriptionAsync(vendorId))
                return new ServiceResponse<SchoolProduct>(null!, success: false, message: "Vendor does not have an active subscription");

            // Get product by its actual ID
          //  var product = await _vR.GetProductByIdAsync(productId);
            var product = await _vR.GetProductByIdAsync(productId);

            if (product == null)
                return new ServiceResponse<SchoolProduct>(null!, success: false, message: "Product not found");

            // Make sure the vendor owns the product
            if (product.VendorId != vendorId)
                return new ServiceResponse<SchoolProduct>(null!, success: false, message: "You can only update your own products");

            // Map update fields from DTO to entity
            _mapper.Map(productDto, product);

            // Update the product in the database
            await _vR.UpdateProductAsync(product);

            // Return updated entity
            return new ServiceResponse<SchoolProduct>(product, true, message: "Product updated successfully");

        }

        public async Task<ServiceResponse<VendorDto>> UpdateVendorByIdAsync(Guid vendorId, VendorDto vendor)
        {
            var vendorUpdate = await _vR.GetVendorByIdAsync(vendorId);
            if (vendorUpdate == null)
                throw new Exception("Vendor not found");

            if (!vendorUpdate.IsApproved)
                throw new Exception("Only approved vendors can create products!");

            if (!await _vR.HasActiveSubscriptionAsync(vendor.Id))
                throw new Exception("Vendor does not have active subscription");

            // Map the updated values from DTO to entity
            _mapper.Map(vendor, vendorUpdate);

            // Update in the database
           await _vR.UpdateVendorAsync(vendorUpdate);


            // Map back to DTO and return success
            var updatedVendor = _mapper.Map<VendorDto>(vendorUpdate);
            return new ServiceResponse<VendorDto>(updatedVendor, success: true, message: "Vendor details updated successfully!");
        }
    }
}