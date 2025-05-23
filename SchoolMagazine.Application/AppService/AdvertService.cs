﻿using AutoMapper;
using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Entities;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Paging;
using SchoolMagazine.Domain.Service_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService
{
    public class AdvertService : IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IPaymentService _paymentService; // Inject Payment Service
        private readonly IMapper _mapper;
        private const decimal CostPerDay = 1000; // ₦1000 per day

        public AdvertService(
            IAdvertRepository advertRepository,
            ISchoolRepository schoolRepository,
            IPaymentService paymentService,
            IMapper mapper)
        {
            _advertRepository = advertRepository;
            _schoolRepository = schoolRepository;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CreateAdvertDto>> CreateAdvertAsync(CreateAdvertDto advertDto)
        {
            var school = await _schoolRepository.GetSchoolByIdAsync(advertDto.SchoolId);
            if (school == null)
            {
                return new ServiceResponse<CreateAdvertDto>(null!, false, "School not found.");
            }

            // Ensure valid date range
            var totalDays = (advertDto.EndDate.Date - advertDto.StartDate.Date).Days;
            if (totalDays < 1)
            {
                return new ServiceResponse<CreateAdvertDto>(null!, false, "Advert must run for at least one day.");
            }

            // Calculate required amount
            var requiredAmount = totalDays * CostPerDay;

            // Check for sufficient payment
            if (advertDto.AmountPaid < requiredAmount)
            {
                return new ServiceResponse<CreateAdvertDto>(null!, false,
                    $"Insufficient payment. Expected ₦{requiredAmount}, but received ₦{advertDto.AmountPaid}.");
            }

            var paymentRequest = new PaymentRequestDto
            {
                AmountPaid = advertDto.AmountPaid,
                PaymentReference = Guid.NewGuid().ToString(), // Unique payment ref
                PaymentDate = DateTime.UtcNow
            };

            // Process payment
            var paymentResult = await _paymentService.ProcessPaymentAsync(paymentRequest);
            if (!paymentResult.IsSuccessful)
            {
                return new ServiceResponse<CreateAdvertDto>(null!, false, "Payment processing failed.");
            }

            // Create and save the advert
            var advert = _mapper.Map<SchoolAdvert>(advertDto);
            advert.IsPaid = true;
            advert.PaymentReference = paymentResult.TransactionId;
            advert.PaymentDate = DateTime.UtcNow;

            await _advertRepository.AddAdvertAsync(advert);

            var advertResponse = _mapper.Map<CreateAdvertDto>(advert);
            return new ServiceResponse<CreateAdvertDto>(advertResponse, true, "Advert created successfully.");
        }

        public async Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> GetPagedAdvertsAsync(int pageNumber, int pageSize)
        {
            var adverts = await _advertRepository.GetPagedAdvertsAsync(pageNumber, pageSize);
            var mappedAdverts = _mapper.Map<List<SchoolAdvertDto>>(adverts.Items);

            return new ServiceResponse<PagedResult<SchoolAdvertDto>>(new PagedResult<SchoolAdvertDto>
            {
                TotalCount = adverts.TotalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = mappedAdverts
            }, true, "Adverts retrieved successfully.");
        }

        public async Task<ServiceResponse<SchoolAdvertDto>> GetAdvertByIdAsync(Guid advertId)
        {
            var advert = await _advertRepository.GetAdvertByIdAsync(advertId);
            if (advert == null)
            {
                return new ServiceResponse<SchoolAdvertDto>(null!, false, "Advert not found.");
            }

            var advertDto = _mapper.Map<SchoolAdvertDto>(advert);
            return new ServiceResponse<SchoolAdvertDto>(advertDto, true, "Advert retrieved successfully.");
        }

        public async Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> GetAdvertsBySchoolIdAsync(Guid schoolId, int pageNumber, int pageSize)
        {
            var adverts = await _advertRepository.GetPagedAdvertsBySchoolIdAsync(schoolId, pageNumber, pageSize);
            var mappedAdverts = _mapper.Map<List<SchoolAdvertDto>>(adverts.Items);

            return new ServiceResponse<PagedResult<SchoolAdvertDto>>(new PagedResult<SchoolAdvertDto>
            {
                TotalCount = adverts.TotalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = mappedAdverts
            }, true, "Adverts retrieved successfully.");
        }

        public async Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> GetAllAdvertsAsync(int pageNumber, int pageSize)
        {
            var adverts = await _advertRepository.GetPagedAdvertsAsync(pageNumber, pageSize);

            if (adverts == null || !adverts.Items.Any())
            {
                return new ServiceResponse<PagedResult<SchoolAdvertDto>>(null!, false, "No adverts found.");
            }

            var mappedAdverts = _mapper.Map<List<SchoolAdvertDto>>(adverts.Items);

            var result = new PagedResult<SchoolAdvertDto>
            {
                TotalCount = adverts.TotalCount,
                PageSize = adverts.PageSize,
                PageNumber = adverts.PageNumber,
                Items = mappedAdverts
            };

            return new ServiceResponse<PagedResult<SchoolAdvertDto>>(result, true, "Adverts retrieved successfully.");
        }

        public async Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> SearchAdvertsAsync(string keyword, int pageNumber, int pageSize)
        {
            var adverts = await _advertRepository.SearchPagedAdvertsAsync(keyword, pageNumber, pageSize);
            var mappedAdverts = _mapper.Map<List<SchoolAdvertDto>>(adverts.Items);

            return new ServiceResponse<PagedResult<SchoolAdvertDto>>(new PagedResult<SchoolAdvertDto>
            {
                TotalCount = adverts.TotalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = mappedAdverts
            }, true, "Search results retrieved successfully.");
        }

        public async Task<ServiceResponse<string>> DeleteAdvertAsync(Guid advertId)
        {
            var result = await _advertRepository.DeleteAdvertAsync(advertId);
            if (!result)
            {
                return new ServiceResponse<string>(null!, false, "Failed to delete advert.");
            }

            return new ServiceResponse<string>("Advert deleted successfully.", true);
        }
    }



}
