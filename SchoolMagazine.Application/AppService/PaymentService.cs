using SchoolMagazine.Application.AppInterface;
using SchoolMagazine.Application.DTOs;
using SchoolMagazine.Domain.Interface;
using SchoolMagazine.Domain.Service_Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService
{

    public class PaymentService : IPaymentService
    {
        private readonly IAdvertRepository _advertRepository;

        public PaymentService(IAdvertRepository advertRepository)
        {
            _advertRepository = advertRepository;
        }

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto paymentDetails)
        {
            if (paymentDetails == null)
            {
                return new PaymentResponseDto
                {
                    Success = false,
                    Message = "Payment details are required."
                };
            }

            // Ensure advert exists
            var advert = await _advertRepository.GetAdvertByIdAsync(paymentDetails.AdvertId);
            if (advert == null)
            {
                return new PaymentResponseDto
                {
                    Success = false,
                    Message = "Advert not found."
                };
            }

            // Simulate payment processing
            bool paymentSuccess = SimulatePaymentProcessing(paymentDetails);

            if (!paymentSuccess)
            {
                return new PaymentResponseDto
                {
                    Success = false,
                    Message = "Payment failed."
                };
            }

            // Update advert as paid
            advert.IsPaid = true;
            advert.PaymentReference = Guid.NewGuid().ToString();
            advert.PaymentDate = DateTime.UtcNow;

            await _advertRepository.UpdateSchoolAdvertAsync(advert);

            return new PaymentResponseDto
            {
                Success = true,
                Message = "Payment successful.",
                TransactionId = advert.PaymentReference
            };
        }

        private bool SimulatePaymentProcessing(PaymentRequestDto paymentDetails)
        {
            // Simulated payment logic (Always returns true for testing)
            return true;
        }
    }

}