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
        private const decimal CostPerDay = 1000; // ₦1000 per day

        public async Task<PaymentResponseDto> ProcessPaymentAsync(PaymentRequestDto paymentRequest)
        {
            // Simulate external payment processing (e.g., Paystack, Flutterwave)
            await Task.Delay(500); // Simulate processing delay

            // Mock a successful payment response
            return new PaymentResponseDto
            {
                IsSuccessful = true,
                Message = "Payment processed successfully.",
                TransactionId = Guid.NewGuid().ToString()
            };
        }

        public decimal CalculateExpectedAmount(DateTime startDate, DateTime endDate)
        {
            int days = (endDate - startDate).Days;
            return days * CostPerDay;
        }

        public bool ValidatePayment(decimal amountPaid, decimal expectedAmount)
        {
            return amountPaid >= expectedAmount;
        }
    }



}