using AutoMapper;
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
        private readonly IPaymentService _paymentService; // Inject payment service
        private readonly IMapper _mapper; // AutoMapper for DTO conversion

        public AdvertService(IAdvertRepository advertRepository, IPaymentService paymentService, IMapper mapper)
        {
            _advertRepository = advertRepository;
            _paymentService = paymentService;
            _mapper = mapper;
        }

       


        public async Task<AdvertServiceResponse<SchoolAdvertDto>> PostAdvertAndPayAsync(SchoolAdvertDto advertDto, PaymentRequestDto paymentDetails)
        {
            // Step 1: Create the Advert (Initially Unpaid)
            var advert = _mapper.Map<SchoolAdvert>(advertDto);
            advert.Id = Guid.NewGuid();
            advert.IsPaid = false; // Initially unpaid

            var addedAdvert = await _advertRepository.AddSchoolAdvertAsync(advert);

            if (!addedAdvert.Success || addedAdvert.Data == null)
            {
                return new AdvertServiceResponse<SchoolAdvertDto>
                {
                    Success = false,
                    Message = "Failed to create advert."
                };
            }

            // Step 2: Assign Advert ID to Payment Details
            paymentDetails.AdvertId = advert.Id;

            // Step 3: Process Payment
            var paymentResponse = await _paymentService.ProcessPaymentAsync(paymentDetails);

            if (!paymentResponse.Success)
            {
                return new AdvertServiceResponse<SchoolAdvertDto>
                {
                    Success = false,
                    Message = "Payment failed: " + paymentResponse.Message
                };
            }

            // Step 4: Update Advert as Paid
            advert.IsPaid = true;
            advert.PaymentReference = paymentResponse.TransactionId ?? throw new Exception("Transaction ID is required.");
            advert.PaymentDate = DateTime.UtcNow;

            await _advertRepository.UpdateSchoolAdvertAsync(advert);

            return new AdvertServiceResponse<SchoolAdvertDto>
            {
                Success = true,
                Message = "Advert posted successfully and payment processed.",
                Data = _mapper.Map<SchoolAdvertDto>(advert)
            };
        }

      
        public async Task<ServiceResponse<PagedResult<SchoolAdvertDto>>> GetAllAdvertsAsync(int pageNumber, int pageSize)
        {
            var pagedAdverts = await _advertRepository.GetAllAdvertsAsync(pageNumber, pageSize);

            if (pagedAdverts.Items.Count == 0)
                return new ServiceResponse<PagedResult<SchoolAdvertDto>>(null!, false, "No adverts found.");

            var advertDtos = _mapper.Map<List<SchoolAdvertDto>>(pagedAdverts.Items);

            var result = new PagedResult<SchoolAdvertDto>
            {
                TotalCount = pagedAdverts.TotalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Items = advertDtos
            };

            return new ServiceResponse<PagedResult<SchoolAdvertDto>>(result, true, "Adverts retrieved successfully.");
        }

    }

}
